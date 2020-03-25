import { Component, OnInit, Input, HostBinding, HostListener } from '@angular/core';

@Component({
	selector: 'app-room',
	templateUrl: './room.component.html',
	styleUrls: ['./room.component.scss'],
})
export class RoomComponent implements OnInit {

	constructor() { }

	@Input() x: number;
	@Input() y: number;

	room: string = undefined;

	@HostBinding('class.active')
	get isActive() {
		return this.room !== undefined;
	}

	ngOnInit(): void {
	}

	@HostListener('click')
	addRoom() {
		this.room = `( RAUM )`;
	}

}
