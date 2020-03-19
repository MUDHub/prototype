import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Subject } from 'rxjs';


@Injectable({
	providedIn: 'root'
})
export class ChatService {

	private connection: signalR.HubConnection;

	messageReceived$: Subject<{user: string, message: string}>;

	constructor() {
		this.connection = new signalR.HubConnectionBuilder().withUrl('http://localhost:5000/chat').build();


		this.connection.on('sendMessage', (user, message) => {
			console.log({ user, message });
			// this.messageReceived$.next({user, message});
		});
		this.connection.start();
	}
}
