import { Component, OnInit } from '@angular/core';
import { ChatService } from './services/chat.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
	title = 'client';

	history: Array<{user: string, message: string}> = [];

	constructor(private chat: ChatService) { }

	ngOnInit(): void {
		this.chat.messageReceived$.subscribe(messageObject => {
			this.history.push(messageObject);
		});
	}



	sendMessage(message: string) {
		this.chat.sendMessage(message);
	}
}
