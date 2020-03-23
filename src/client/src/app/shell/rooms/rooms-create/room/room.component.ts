import { Component, OnInit, Input, HostBinding, HostListener } from '@angular/core';

@Component({
	selector: 'app-room',
	templateUrl: './room.component.html',
	styleUrls: ['./room.component.scss'],
})
export class RoomComponent implements OnInit {

	@Input() x: number;
	@Input() y: number;

	room: string = undefined;

	constructor() { }

	ngOnInit(): void {
	}


	@HostListener('click')
	addRoom() {
		this.room = `Raum (${this.x}, ${this.y})`;
	}

}
