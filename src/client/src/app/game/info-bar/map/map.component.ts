import { Component, OnInit } from '@angular/core';
import { RoomService } from 'src/app/services/room.service';
import { IRoom } from "src/app/model/IRoom";
import { GameService } from 'src/app/services/game.service';

@Component({
	selector: 'app-map',
	templateUrl: './map.component.html',
	styleUrls: ['./map.component.scss']
})
export class MapComponent implements OnInit {

	activeRoom: string;
	rooms: IRoom[];
	map: IRoom[][] = [
		[ undefined ]
	];

	private get width() {
		return this.map[0]?.length;
	}
	private get height() {
		return this.map.length;
	}

	constructor(private roomService: RoomService, private game: GameService) { }

	async ngOnInit() {
		this.rooms = await this.roomService.getRooms();
		this.renderMap();

		this.game.changeRoom$.subscribe(roomId => {
			this.activeRoom = roomId;
		});
	}


	private renderMap() {
		for (const room of this.rooms) {
			while (this.height - 1 < room.position.y) {
				this.map.push(Array(this.width).fill(undefined));
			}

			while (this.width - 1 < room.position.x) {
				this.map.forEach(row => {
					row.push(undefined);
				});
			}

			this.map[room.position.y][room.position.x] = room;
		}

		console.log(this.map);
	}




}
