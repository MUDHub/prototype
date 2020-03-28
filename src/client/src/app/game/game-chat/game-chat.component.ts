import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/services/game.service';
import { MessageType } from "src/app/model/MessageType";

@Component({
	selector: 'app-game-chat',
	templateUrl: './game-chat.component.html',
	styleUrls: ['./game-chat.component.scss']
})
export class GameChatComponent implements OnInit {

	MessageType = MessageType;

	chat: { message: string, type: MessageType }[] = [
		{
			message: 'Gebe "hilfe" für eine Übersicht aller Befehle ein',
			type: MessageType.Server
		}
	];

	constructor(private game: GameService) { }

	ngOnInit(): void {
		this.game.receiveGameMessage$.subscribe(message => {
			this.chat.push(message);
		});

		this.game.onClear.subscribe(() => {
			this.chat = [];
		});
	}

}
