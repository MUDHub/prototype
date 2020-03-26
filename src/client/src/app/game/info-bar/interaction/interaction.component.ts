import { Component, OnInit } from '@angular/core';
import { GameService, Direction } from 'src/app/services/game.service';

@Component({
	selector: 'app-interaction',
	templateUrl: './interaction.component.html',
	styleUrls: ['./interaction.component.scss']
})
export class InteractionComponent implements OnInit {

	constructor(private game: GameService) { }

	ngOnInit(): void {
	}


	async sendMessage(element: HTMLInputElement) {
		const message = element.value;

		this.game.sendUserInput(message);

		const command = message.split(' ')[0];

		switch (command) {
			case 'gehe':
				const arg = message.split(' ')[1];
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


			default:
				console.log('unrecognized command');
				break;
		}


		element.value = '';
	}

}
