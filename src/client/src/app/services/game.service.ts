import { Injectable, EventEmitter } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment as env } from 'src/environments/environment';
import { AuthService } from './auth.service';
import { Subject, Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class GameService {

	public onClear = new EventEmitter();

	private receiveGameMessageSubject = new Subject<{ message: string, type: MessageType }>();
	public receiveGameMessage$ = this.receiveGameMessageSubject.asObservable();


	private connection: signalR.HubConnection;

	constructor(private auth: AuthService) {
		this.connection = new signalR.HubConnectionBuilder()
			.withUrl(env.url + 'hubs/game', {
				skipNegotiation: true,
				transport: signalR.HttpTransportType.WebSockets,
				accessTokenFactory: () => this.auth.token
			})
			.build();


		this.connection.on('receiveGameMessage', (message) => {
			console.log('received game message', message);
			this.receiveGameMessageSubject.next({message, type: MessageType.Server});
		});


		this.connection.start();
	}

	async tryEnterRoom(direction: Direction) {
		const result = await this.connection.invoke<{ message: string, succeeded: boolean }>('tryEnterRoom', direction);
		if (!result.succeeded) {
			this.receiveGameMessageSubject.next({ message: `Es befindet sich kein Raum im ${Direction[direction]}`, type: MessageType.Server});
		}
	}


	public sendUserInput(message: string) {
		this.receiveGameMessageSubject.next({ message, type: MessageType.Client });
	}

	public clearChat() {
		this.onClear.emit();
	}

}


export enum Direction { North, East, West, South }
export enum MessageType { Server, Client }
