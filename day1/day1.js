var realInput = "R5, R4, R2, L3, R1, R1, L4, L5, R3, L1, L1, R4, L2, R1, R4, R4, L2, L2, R4, L4, R1, R3, L3, L1, L2, R1, R5, L5, L1, L1, R3, R5, L1, R4, L5, R5, R1, L185, R4, L1, R51, R3, L2, R78, R1, L4, R188, R1, L5, R5, R2, R3, L5, R3, R4, L1, R2, R2, L4, L4, L5, R5, R4, L4, R2, L5, R2, L1, L4, R4, L4, R2, L3, L4, R2, L3, R3, R2, L2, L3, R4, R3, R1, L4, L2, L5, R4, R4, L1, R1, L5, L1, R3, R1, L2, R1, R1, R3, L4, L1, L3, R2, R4, R2, L2, R1, L5, R3, L3, R3, L1, R4, L3, L3, R4, L2, L1, L3, R2, R3, L2, L1, R4, L3, L5, L2, L4, R1, L4, L4, R3, R5, L4, L1, L1, R4, L2, R5, R1, R1, R2, R1, R5, L1, L3, L5, R2";

var tests = {
	'R2, R3': 5,
	'R2, R2, R2': 2,
	'R5, L5, R5, R3': 12
};

for(var input in tests){
	var output = calculate(input);
	var answer = tests[input];
	console.log(input, output, answer, output == answer ? 'CORRECT' : 'INCORRECT!');
};

console.log("ANSWER:", calculate(realInput));

function calculate(input){
	var x = 0, y = 0;
	var heading = 0;

	input.split(', ').forEach(instruction => {
		//determine direction
		var direction = instruction[0];
		
		if(direction === 'L'){
			heading = (4 + heading - 1) % 4;
		}else if(direction === 'R'){
			heading = (4 + heading + 1) % 4;
		}else{
			throw "Parse error";
		}

		//determine number of steps
		var steps = parseInt(instruction.substr(1, instruction.length - 1));

		if(heading == 0 || heading == 2){
			y -= (heading - 1) * steps;	
		}else{
			x -= (heading - 2) * steps;	
		}
	});

	return Math.abs(x) + Math.abs(y);
}
