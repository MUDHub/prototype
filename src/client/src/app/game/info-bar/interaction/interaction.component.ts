import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/services/game.service';
import { Direction } from 'src/app/model/Direction';
import { RoomService } from 'src/app/services/room.service';

@Component({
	selector: 'app-interaction',
	templateUrl: './interaction.component.html',
	styleUrls: ['./interaction.component.scss']
})
export class InteractionComponent implements OnInit {

	constructor(private game: GameService, private room: RoomService) { }

	ngOnInit(): void {
	}


	async sendMessage(element: HTMLInputElement) {
		const message = element.value;

		this.game.sendUserInput(message);

		const command = message.split(' ')[0];
		const arg = message.replace(command + ' ', '');

		switch (command) {
			case 'gehe':
				let direction: Direction;
				switch (arg) {
					case 'westen':
						direction = Direction.West;
						break;
					case 'osten':
						direction = Direction.East;
						break;
					case 'norden':
						direction = Direction.North;
						break;
					case 'süden':
						direction = Direction.South;
						break;
				}
				const result = await this.game.tryEnterRoom(direction);
				console.log(result);
				break;

			case '#clear':
				this.game.clearChat();
				break;


			case 'untersuche':
				await this.handleUntersuche(arg);
				break;

			default:
				console.log('unrecognized command');
				break;
		}


		element.value = '';
	}




	async handleUntersuche(subject: string) {
		switch (subject) {
			case 'ausgänge':
				const neighbours = await this.room.getAdjacentRooms(this.game.actualRoom);
				for (const neighbour of neighbours) {
					let dir;
					switch (neighbour.direction) {
						case 'North':
							dir = 'Norden';
							break;
						case 'East':
							dir = 'Osten';
							break;
						case 'West':
							dir = 'Westen';
							break;
						case 'South':
							dir = 'Süden';
							break;
					}

					this.game.displayMessage(`Im ${dir}: ${neighbour.room.description}`);
				}
				break;
			default:
				alert('Weiß nicht was zu untersuchen... Kenne ' + subject + ' nicht');
				break;
		}
	}

}
