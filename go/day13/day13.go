package main

import "strconv"

func compute(n int, destination Coordinate, part2 bool) int {
	m := newMaze(n)
	if part2 {
		return m.numberofMaxSteps(Coordinate{1, 1}, 50)
	}
	return m.shortestPath(Coordinate{1, 1}, destination)
}

type Coordinate struct {
	x, y int
}

func (c *Coordinate) toTile(steps int) Tile {
	return Tile{x: c.x, y: c.y, steps: steps}
}

type Tile struct {
	x, y  int
	steps int
}

func (t *Tile) toCoordinate() Coordinate {
	return Coordinate{x: t.x, y: t.y}
}

type Queue struct {
	list []Tile
}

func newQueue() Queue {
	return Queue{list: make([]Tile, 0)}
}

func (q *Queue) empty() bool {
	return len(q.list) == 0
}

func (q *Queue) push(c Tile) {
	q.list = append(q.list, c)
}

func (q *Queue) pushList(tiles []Tile) {
	for _, c := range tiles {
		q.list = append(q.list, c)
	}
}

func (q *Queue) pop() Tile {
	if q.empty() {
		panic("Queue is empty")
	}

	v := q.list[0]
	q.list = q.list[1:]
	return v
}

type Maze struct {
	number   int
	explored map[Coordinate]bool
	queue    Queue
}

func newMaze(number int) *Maze {
	m := &Maze{
		number:   number,
		explored: make(map[Coordinate]bool),
		queue:    newQueue(),
	}
	return m
}

func (m *Maze) isExplored(c Coordinate) bool {
	_, explored := m.explored[c]
	return explored
}

func (m *Maze) numberofMaxSteps(startPos Coordinate, maxSteps int) int {
	count := 0
	start := startPos.toTile(0)
	m.explore(start)
	m.queue.push(start)
	for !m.queue.empty() {
		pos := m.queue.pop()
		count++
		if pos.steps == maxSteps {
			continue
		}

		neighbours := m.getNeighbours(pos)
		m.queue.pushList(neighbours)
	}

	return count
}

func (m *Maze) shortestPath(startPos Coordinate, destination Coordinate) int {
	start := startPos.toTile(0)
	m.explore(start)
	m.queue.push(start)
	for !m.queue.empty() {
		pos := m.queue.pop()
		if pos.toCoordinate() == destination {
			return pos.steps
		}

		neighbours := m.getNeighbours(pos)
		m.queue.pushList(neighbours)
	}
	panic("Couldn't find destination")
}

func (m *Maze) explore(t Tile) bool {
	c := t.toCoordinate()
	_, explored := m.explored[c]
	if !explored {
		m.explored[c] = true
	}
	return explored
}

func (m *Maze) getNeighbours(c Tile) []Tile {
	neighbours := make([]Tile, 0)

	if c.x > 0 {
		left := Tile{x: c.x - 1, y: c.y, steps: c.steps + 1}
		if !m.isWallT(left) && !m.explore(left) {
			neighbours = append(neighbours, left)
		}
	}
	right := Tile{x: c.x + 1, y: c.y, steps: c.steps + 1}
	if !m.isWallT(right) && !m.explore(right) {
		neighbours = append(neighbours, right)
	}
	if c.y > 0 {
		up := Tile{x: c.x, y: c.y - 1, steps: c.steps + 1}
		if !m.isWallT(up) && !m.explore(up) {

			neighbours = append(neighbours, up)
		}
	}
	down := Tile{x: c.x, y: c.y + 1, steps: c.steps + 1}
	if !m.isWallT(down) && !m.explore(down) {
		neighbours = append(neighbours, down)
	}

	return neighbours
}

func (m *Maze) isWallT(t Tile) bool {
	return m.isWall(t.x, t.y)
}

func (m *Maze) isWall(x, y int) bool {
	n := x*x + 3*x + 2*x*y + y + y*y
	n += m.number
	ones := countOnesInBinary(n)
	return ones%2 != 0
}

func countOnesInBinary(n int) int {
	s := strconv.FormatInt(int64(n), 2)
	c := 0
	for _, chr := range s {
		if string(chr) == "1" {
			c++
		}
	}
	return c
}
