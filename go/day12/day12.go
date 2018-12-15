package main

import (
	"regexp"
	"strconv"
	"strings"
)

func compute(input string, part2 bool) int {
	computer := newComputer([]string{"a", "b", "c", "d"}, []*Instruction{instrCpy, instrInc, instrDec, instrJnz})
	if part2 {
		computer.reg["c"] = 1
	}
	computer.executeProgram(input)
	return computer.reg["a"]
}

type Registers map[string]int

type Computer struct {
	reg          Registers      //registers
	pc           int            //program counter
	program      []ProgramLine  //program to be executed
	instructions []*Instruction //available instruction
}

func (c *Computer) executeProgram(code string) {
	c.program = c.parseProgram(code)

	for {
		if c.pc < 0 || c.pc >= len(c.program) {
			break
		}

		instr := c.program[c.pc]
		instr.execute(c)
	}
}

func (c *Computer) parseProgram(program string) []ProgramLine {
	split := strings.Split(program, "\n")
	p := make([]ProgramLine, 0)
	for _, line := range split {
		if line == "" {
			continue
		}
		pl := c.parseProgramLine(line)
		p = append(p, *pl)
	}
	return p
}

func (c *Computer) parseProgramLine(line string) *ProgramLine {
	for _, instr := range c.instructions {
		ok, line := instr.parse(line)
		if ok {
			return line
		}
	}

	panic("Couldn't parse line: " + line)
}

func (c *Computer) getValue(arg string) int {
	if isNumber(arg) {
		v, _ := strconv.Atoi(arg)
		return v
	}

	return c.reg[arg]
}

type ProgramLine struct {
	arguments   []string
	instruction *Instruction
}

func (pl *ProgramLine) execute(c *Computer) {
	pl.instruction.executeFunc(c, pl.arguments)
}

type Instruction struct {
	executeFunc func(*Computer, []string)
	parser      *regexp.Regexp
}

func newInstruction(parser *regexp.Regexp, executeFunc func(*Computer, []string)) *Instruction {
	i := &Instruction{parser: parser, executeFunc: executeFunc}
	return i
}

func (i *Instruction) parse(code string) (ok bool, arguments *ProgramLine) {
	matches := i.parser.FindAllStringSubmatch(code, 10)
	if len(matches) == 0 {
		return false, nil
	}

	pl := &ProgramLine{arguments: matches[0][1:], instruction: i}
	return true, pl
}

var instrCpy = newInstruction(regexp.MustCompile(`cpy (\w+|-?\d+) (\w+)`),
	func(c *Computer, args []string) {
		val := c.getValue(args[0])
		c.reg[args[1]] = val
		c.pc++
	},
)
var instrInc = newInstruction(regexp.MustCompile(`inc (\w+)`),
	func(c *Computer, args []string) {
		c.reg[args[0]]++
		c.pc++
	},
)
var instrDec = newInstruction(regexp.MustCompile(`dec (\w+)`),
	func(c *Computer, args []string) {
		c.reg[args[0]]--
		c.pc++
	},
)
var instrJnz = newInstruction(regexp.MustCompile(`jnz (\w+|-?\d+) (-?\d+)`),
	func(c *Computer, args []string) {
		val := c.getValue(args[0])
		if val != 0 {
			c.pc += c.getValue(args[1])
		} else {
			c.pc++
		}
	},
)

func newComputer(registers []string, instr []*Instruction) *Computer {
	c := &Computer{reg: make(Registers)}
	for _, regName := range registers {
		c.reg[regName] = 0
	}

	c.instructions = instr

	return c
}

func isNumber(i string) bool {
	_, err := strconv.Atoi(i)
	return err == nil
}
