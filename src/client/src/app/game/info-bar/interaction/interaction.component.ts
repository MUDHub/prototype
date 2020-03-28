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

	commandHistory: string[] = [];

	input = '';

	private _historyPosition = -1;
	private get historyPosition() {
		return this._historyPosition;
	}
	private set historyPosition(value: number) {
		if (value >= -1 && value < this.commandHistory.length) {
			this._historyPosition = value;
			if (value === -1) {
				this.input = '';
			} else {
				this.input = this.commandHistory[this.historyPosition];
			}
		}
	}

	constructor(private game: GameService, private room: RoomService) { }

	ngOnInit(): void {
	}


	async sendMessage() {
		const message = this.input;
		this.addToHistory(message);
		this.game.sendUserInput(message);

		const command = message.split(' ')[0];
		const arg = message.replace(command + ' ', '');

		switch (command) {
			case 'g':
			case 'gehe':
				let direction: Direction;
				switch (arg) {
					case 'w':
					case 'westen':
						direction = Direction.West;
						break;
					case 'o':
					case 'osten':
						direction = Direction.East;
						break;
					case 'n':
					case 'norden':
						direction = Direction.North;
						break;
					case 's':
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


			case 'hilfe':
				this.handleHilfe();
				break;


			case 'untersuche':
				await this.handleUntersuche(arg);
				break;

			default:
				console.log('unrecognized command');
				break;
		}


		this.input = '';
	}



	handleHilfe() {
		const commands: { command: string, parameter?: string[], description: string }[] = [
			{
				command: 'gehe',
				parameter: ['norden', 'osten', 'süden', 'westen', ],
				description: 'Bewegt den Spieler in den angrenzenden Raum in der jeweiligen Himmelsrichtung'
			},
			{
				command: 'untersuche',
				parameter: [ 'ausgänge' ],
				description: 'Gibt Informationen über angrenzende Räume aus'
			},
			{
				command: '#clear',
				description: 'Löscht die Chat-Historie'
			},
		];

		for (const c of commands) {
			this.game.displayMessage(` ${c.command} ${c.parameter ? c.parameter?.join('|') : ''}`);
			this.game.displayMessage(`--> ${c.description}`);
		}

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



	handleHistory(dir: 'up' | 'down') {
		if (dir === 'up') {
			this.historyPosition++;
		} else {
			this.historyPosition--;
		}
	}

	addToHistory(message) {
		this.commandHistory.unshift(message);
		this.historyPosition = -1;
	}
}
