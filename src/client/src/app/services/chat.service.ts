import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { Subject } from 'rxjs';

import { environment as env } from 'src/environments/environment';


@Injectable({
	providedIn: 'root'
})
export class ChatService {

	private connection: signalR.HubConnection;
	roomNr: number;

	messageReceived$ = new Subject<string>();

	constructor() {
		this.connection = new signalR.HubConnectionBuilder().withUrl(env.signalrUrl).build();

		this.connection.on('receiveRoom', (message) => {
			this.messageReceived$.next(message);
		});
		this.connection.start();
	}


	sendMessage(message: string) {
		if (this.roomNr) {
			this.connection.invoke('sendToRoom', this.roomNr, message);
		} else {
			throw new Error('No room specified');
		}
	}



	changeRoom(roomNr: number) {
		this.roomNr = roomNr;
		// this.connection.invoke('changeRoom', roomNr);
	}
}
