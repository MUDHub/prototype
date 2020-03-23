import { Component, OnInit } from '@angular/core';
import { ChatService } from '../services/chat.service';

@Component({
	selector: 'app-chat',
	templateUrl: './chat.component.html',
	styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

	name: string;

	chatHistory: Array<{username: string, message: string, timestamp: Date}> = [];

	constructor(private chatService: ChatService) { }

	ngOnInit() {
		this.chatService.newMessage$.subscribe(m => this.chatHistory.unshift(m));
	}


	async sendMessage(message: HTMLInputElement) {
		if (message.value) {
			try {
				await this.chatService.sendMessage(this.name, message.value);
				message.value = '';
			} catch (err) {
				console.log(err);
				alert('Fehler beim senden\n' + err.message);
			}
		}
	}
}
