import { Component, OnInit } from '@angular/core';
import { ChatService } from './services/chat.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
	title = 'client';

	history: Array<string> = [];


	constructor(public chat: ChatService) { }

	ngOnInit(): void {
		this.chat.messageReceived$.subscribe(message => {
			this.history.push(message);
		});
	}


	sendMessage(message: string) {
		this.chat.sendMessage(message);
	}


	changeRoom(roomNr: number) {
		console.log('change room to ', roomNr);
		this.chat.changeRoom(roomNr);
		this.history = [];
	}
}
