package main

import (
	"fmt"
	"regexp"
	"strconv"
	"strings"
)

type PART int

const (
	PART1 PART = iota
	PART2 PART = iota
)

func compute(input string, part PART) *Factory {
	factory := NewFactory()

	for _, line := range strings.Split(input, "\n") {
		factory.executeCommand(line)
	}
	return factory
}

type Receiver struct {
	id    int
	rType string

	lowValue, highValue int
	low, high           *Receiver
}

func (v *Receiver) getValues() []int {
	return []int{v.lowValue, v.highValue}
}

func (v *Receiver) give(value int) {

	fmt.Println(v.rType, v.id, "received value", value)

	if v.lowValue == 0 {
		v.lowValue = value
	} else if v.highValue == 0 {
		v.highValue = value
	} else {
		panic("Receiver already has two values")
	}

	if v.lowValue > v.highValue {
		v.lowValue, v.highValue = v.highValue, v.lowValue
	}

	if v.lowValue == 17 && v.highValue == 61 {
		fmt.Println(v.rType, v.id, "is the droid you are looking for")
	}

	v.run()
}

func (v *Receiver) run() {
	if v.isFull() {
		fmt.Println(v.rType, v.id, "is full. Giving away:")
		if v.low != nil {
			v.low.give(v.lowValue)
			v.lowValue = 0
		} else {
			fmt.Println(v.rType, v.id, "had a nil low")
		}
		if v.high != nil {
			v.high.give(v.highValue)
			v.highValue = 0
		} else {
			fmt.Println(v.rType, v.id, "had a nil high")
		}
	}
}

func (v *Receiver) isEmpty() bool {
	return v.lowValue == 0 && v.highValue == 0
}

func (v *Receiver) isFull() bool {
	return v.lowValue != 0 && v.highValue != 0
}

func (f *Factory) getReceiver(id int, receiveType string) *Receiver {
	if receiveType == "bot" {
		return f.getBot(id)
	} else if receiveType == "output" {
		return f.getOutput(id)
	} else {
		panic("Receive type doesnt exist")
	}
}

func (f *Factory) getBot(id int) *Receiver {
	if _, ok := f.bots[id]; !ok {
		f.bots[id] = &Receiver{id: id, rType: "bot"}
	}
	return f.bots[id]
}

func (f *Factory) getOutput(id int) *Receiver {
	if _, ok := f.outputs[id]; !ok {
		f.outputs[id] = &Receiver{id: id, rType: "output"}
	}
	return f.outputs[id]
}

type Factory struct {
	bots    map[int]*Receiver
	outputs map[int]*Receiver
}

func NewFactory() *Factory {
	return &Factory{
		bots:    make(map[int]*Receiver),
		outputs: make(map[int]*Receiver),
	}
}

func (f *Factory) getValues() map[int][]int {
	values := make(map[int][]int)
	for k, v := range f.outputs {
		if !v.isEmpty() {
			values[k] = v.getValues()
		}
	}
	return values
}

var valueGoes = regexp.MustCompile(`value (\d+) goes to bot (\d+)`)
var giveValue = regexp.MustCompile(`bot (\d+) gives low to (bot|output) (\d+) and high to (bot|output) (\d+)`)

func (f *Factory) executeCommand(command string) {
	if len(command) == 0 {
		return
	}

	if command[0:1] == "v" {
		args, _ := parseCommand(valueGoes, command)
		bot := f.getBot(args[1])
		bot.give(args[0])
		bot.run()
	} else if command[0:1] == "b" {
		args, receivers := parseCommand(giveValue, command)
		bot := f.getBot(args[0])
		bot.low = f.getReceiver(args[1], receivers[0])
		bot.high = f.getReceiver(args[2], receivers[1])
		bot.run()
	} else {
		panic("Can't parse command: " + command)
	}
}

func parseCommand(pattern *regexp.Regexp, command string) (arguments []int, receivers []string) {
	matches := pattern.FindStringSubmatch(command)
	if len(matches) == 0 {
		panic("No matches" + command)
	}

	for i := 1; i < len(matches); i++ {
		val, err := strconv.Atoi(matches[i])
		if err == nil {
			arguments = append(arguments, val)
		} else {
			receivers = append(receivers, matches[i])
		}
	}

	return arguments, receivers
}
