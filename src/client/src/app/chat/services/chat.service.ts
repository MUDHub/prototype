import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment as env } from 'src/environments/environment';
import { Subject } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class ChatService {

	private connection: signalR.HubConnection;

	private newMessageSubject = new Subject<{ username: string, message: string, timestamp: Date }>();
	public newMessage$ = this.newMessageSubject.asObservable();

	constructor() {
		this.connection = new signalR.HubConnectionBuilder()
			.withUrl(env.url + 'chat')
			.build();

		this.connection.on('receiveMessage', (name, message, timestamp) => {
			this.newMessageSubject.next({ username: name, message, timestamp });
		});



		this.connection.start();
	}


	public async sendMessage(username: string, message: string) {
		await this.connection.invoke('sendMessage', username, message);
	}
}
