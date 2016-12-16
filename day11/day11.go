package main

import (
	"fmt"
	"regexp"
	"strings"
)

const maxObjects = 10

func compute(input string) int {
	state := createInitialState(input)
	fmt.Printf("%+v", state)
	return 5
}

type State struct {
	elevator  int
	pairs     map[string]*Locations
	numFloors int
}

func (s *State) getFloors() [][]Object {
	floors := make([][]Object, s.numFloors)
	for name, pair := range s.pairs {
		floors[pair.chipLocation] = append(floors[pair.chipLocation], Object{name: name, typ: "chip"})
		floors[pair.generatorLocation] = append(floors[pair.generatorLocation], Object{name: name, typ: "generator"})
	}
	return floors
}

func (s *State) isValid() bool {
	type props struct {
		hasGenerator, hasUnshielded bool
	}
	floors := make([]props, s.numFloors)

	for _, pair := range s.pairs {
		floors[pair.generatorLocation].hasGenerator = true

		if !pair.isShielded() {
			floors[pair.chipLocation].hasUnshielded = true
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

type Object struct {
	name string
	typ  string //chip or generator
}

type Locations struct {
	chipLocation      int
	generatorLocation int
}

func (loc *Locations) isShielded() bool {
	return loc.chipLocation == loc.generatorLocation || loc.chipLocation == 0
}

var chipRegex = regexp.MustCompile(`(\w+?)-compatible microchip`)
var generatorRegex = regexp.MustCompile(`(\w+?) generator`)

func createInitialState(input string) *State {
	state := State{pairs: make(map[string]*Locations), elevator: 1}
	lines := strings.Split(input, "\n")
	state.numFloors = len(lines)
	for floor, line := range lines {

		chipMatches := chipRegex.FindAllStringSubmatch(line, maxObjects)
		if len(chipMatches) > 0 {
			for _, match := range chipMatches {
				name := match[1]
				if _, exists := state.pairs[name]; !exists {
					state.pairs[name] = &Locations{chipLocation: floor}
				} else {
					state.pairs[name].chipLocation = floor
				}
			}
		}

		generatorMatches := generatorRegex.FindAllStringSubmatch(line, maxObjects)
		if len(generatorMatches) > 0 {
			for _, match := range generatorMatches {
				name := match[1]
				if _, exists := state.pairs[name]; !exists {
					state.pairs[name] = &Locations{generatorLocation: floor}
				} else {
					state.pairs[name].generatorLocation = floor
				}
			}
		}
	}
	return &state
}
