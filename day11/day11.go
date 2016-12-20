package main

import (
	"math"
	"regexp"
	"strings"

	"github.com/mitchellh/hashstructure"
)

const maxObjects = 10

type Direction int

const (
	UP   Direction = 1
	DOWN Direction = -1
)

func compute(input string) int {
	state := createInitialState(input)
	solver := Solver{visited: make(map[uint64]bool)}
	return solver.getMinMoves(state)
}

type Solver struct {
	visited map[uint64]bool //state cache
}

func (s *Solver) getMinMoves(state *State) int {

	if state.isEndState() {
		return state.moves
	}

	//prevent loops
	hash := state.getHash()
	s.visited[hash] = true

	children := state.generateValidChildStates()

	//get minimum of children
	minValue := math.MaxInt64

	for _, child := range children {

		//get from cache
		// childHash := child.getHash()
		// if _, visited := s.visited[childHash]; !visited {
		val := s.getMinMoves(child)
		if val < minValue {
			minValue = val
		}
		// }
	}

	return minValue
}

type State struct {
	elevator  int
	pairs     map[string]*Locations
	numFloors int
	moves     int
}

func (s *State) takeObjects(objects []*Object, dir Direction) {
	for _, obj := range objects {
		locs := s.pairs[obj.name]

		if obj.typ == Generator {
			if locs.genLoc != s.elevator {
				panic("Lift not on same level as object")
			}
			locs.genLoc += int(dir)
		} else if obj.typ == Chip {
			if locs.chipLoc != s.elevator {
				panic("Lift not on same level as object")
			}
			locs.chipLoc += int(dir)
		}
	}
	s.elevator += int(dir)
}

func (s *State) copy() *State {
	pairsCopy := make(map[string]*Locations)
	for key, val := range s.pairs {
		pairsCopy[key] = &Locations{chipLoc: val.chipLoc, genLoc: val.genLoc}
	}
	copy := State{elevator: s.elevator, numFloors: s.numFloors, moves: s.moves, pairs: pairsCopy}
	return &copy
}

func (s *State) getHash() uint64 {
	cpy := s.copy()
	cpy.moves = 0
	h, _ := hashstructure.Hash(cpy, nil)
	return h
}

func (s *State) isEndState() bool {
	endFloor := s.numFloors - 1
	for _, pair := range s.pairs {
		if pair.chipLoc != endFloor || pair.genLoc != endFloor {
			return false
		}
	}

	return true
}

func (s *State) isValid() bool {
	type props struct {
		hasGenerator, hasUnshielded bool
	}
	floors := make([]props, s.numFloors)

	for _, pair := range s.pairs {
		floors[pair.genLoc].hasGenerator = true

		if !pair.isShielded() {
			floors[pair.chipLoc].hasUnshielded = true
		}
	}

	//check if floor has an unshielded chip and a different generator
	for _, floor := range floors {
		if floor.hasGenerator && floor.hasUnshielded {
			return false
		}
	}

	return true
}

func (s *State) generateValidChildStates() []*State {
	states := make([]*State, 0)

	//combinations of items to take at current floor
	itemCombinations := s.generateObjectCombinations(s.elevator)

	for _, items := range itemCombinations {
		//go up
		if s.elevator < s.numFloors-1 {
			cpy := s.copy()
			cpy.moves++
			cpy.takeObjects(items, UP)
			if cpy.isValid() {
				states = append(states, cpy)
			}
		}

		//go down
		if s.elevator > 0 {
			cpy := s.copy()
			cpy.moves++
			cpy.takeObjects(items, DOWN)
			if cpy.isValid() {
				states = append(states, cpy)
			}
		}
	}

	return states
}

func (s *State) generateObjectCombinations(floor int) [][]*Object {
	//get objects from floor
	objects := make([]Object, 0)

	for name, locs := range s.pairs {
		if locs.chipLoc == floor {
			objects = append(objects, Object{name: name, typ: Chip})
		}
		if locs.genLoc == floor {
			objects = append(objects, Object{name: name, typ: Generator})
		}
	}

	combinations := make([][]*Object, 0)

	//take 2 items
	for i := 0; i < len(objects); i++ {
		for j := i + 1; j < len(objects); j++ {
			if i == j {
				continue
			}
			comb := []*Object{&objects[i], &objects[j]}
			combinations = append(combinations, comb)
		}
	}

	//take 1 items
	for i := 0; i < len(objects); i++ {
		combinations = append(combinations, []*Object{&objects[i]})
	}

	return combinations
}

type ObjectType string

const (
	Chip      ObjectType = "chip"
	Generator ObjectType = "generator"
)

type Object struct {
	name string
	typ  ObjectType
}

type Locations struct {
	chipLoc int
	genLoc  int
}

func (loc *Locations) isShielded() bool {
	return loc.chipLoc == loc.genLoc || loc.chipLoc == 0
}

var chipRegex = regexp.MustCompile(`(\w+?)-compatible microchip`)
var generatorRegex = regexp.MustCompile(`(\w+?) generator`)

func createInitialState(input string) *State {
	state := State{pairs: make(map[string]*Locations), elevator: 0}
	lines := strings.Split(input, "\n")
	state.numFloors = len(lines)
	for floor, line := range lines {

		chipMatches := chipRegex.FindAllStringSubmatch(line, maxObjects)
		if len(chipMatches) > 0 {
			for _, match := range chipMatches {
				name := match[1]
				if _, exists := state.pairs[name]; !exists {
					state.pairs[name] = &Locations{chipLoc: floor}
				} else {
					state.pairs[name].chipLoc = floor
				}
			}
		}

		generatorMatches := generatorRegex.FindAllStringSubmatch(line, maxObjects)
		if len(generatorMatches) > 0 {
			for _, match := range generatorMatches {
				name := match[1]
				if _, exists := state.pairs[name]; !exists {
					state.pairs[name] = &Locations{genLoc: floor}
				} else {
					state.pairs[name].genLoc = floor
				}
			}
		}
	}
	return &state
}
