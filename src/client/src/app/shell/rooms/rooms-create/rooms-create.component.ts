import { Component, OnInit } from '@angular/core';

@Component({
	selector: 'app-rooms-create',
	templateUrl: './rooms-create.component.html',
	styleUrls: ['./rooms-create.component.scss']
})
export class RoomsCreateComponent implements OnInit {

	matrix: string[][];

	get width() {
		return this.matrix[0].length;
	}

	get height() {
		return this.matrix.length;
	}

	constructor() { }

	ngOnInit(): void {
		this.generateMatrix(3, 7);
	}

	private generateMatrix(width: number, height: number) {
		const rows = [];

		for (let y = 0; y < height; y++) {
			const rooms = [];
			for (let x = 0; x < width; x++) {
				rooms.push(undefined);
			}
			rows.push(rooms);
		}

		this.matrix = rows;
	}

	addWidth() {
		this.matrix.forEach(row => {
			row.push(undefined);
		});
	}

	removeWidth() {
		if (this.width > 1) {
			this.matrix.forEach(row => {
				row.pop();
			});
		}
	}

	addHeight() {
		this.matrix.push(new Array(this.width).fill(undefined));
	}

	removeHeight() {
		if (this.height > 1) {
			this.matrix.pop();
		}
	}

}
