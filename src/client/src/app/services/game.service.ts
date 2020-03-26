import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment as env } from 'src/environments/environment';
import { AuthService } from './auth.service';
import { Subject, Observable } from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class GameService {


	private receiveGameMessageSubject = new Subject<string>();
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
			this.receiveGameMessageSubject.next(message);
		});


		this.connection.start();
	}

	async tryEnterRoom(direction: Direction) {
		const result = await this.connection.invoke<{ message: string, succeeded: boolean }>('tryEnterRoom', direction);
		if (!result.succeeded) {
			this.receiveGameMessageSubject.next(`Es befindet sich kein Raum im ${Direction[direction]}`);
		}
	}

}


export enum Direction { North, East, West, South }
