import { Component, OnInit } from '@angular/core';
import { GameService } from 'src/app/services/game.service';

@Component({
	selector: 'app-game-chat',
	templateUrl: './game-chat.component.html',
	styleUrls: ['./game-chat.component.scss']
})
export class GameChatComponent implements OnInit {

	chat: string[] = [];

	constructor(private game: GameService) { }

	ngOnInit(): void {
		this.game.receiveGameMessage$.subscribe(message => {
			this.chat.push(message);
		});
	}

}
