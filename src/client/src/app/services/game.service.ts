import { Injectable, EventEmitter } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment as env } from 'src/environments/environment';
import { AuthService } from './auth.service';
import { Subject, Observable, BehaviorSubject } from 'rxjs';
import { Direction } from '../model/Direction';
import { MessageType } from '../model/MessageType';
import { NavigationResult } from '../model/NavigationResult';

@Injectable({
	providedIn: 'root'
})
export class GameService {
	private changeRoomSubject = new BehaviorSubject<string>(undefined);
	public changeRoom$ = this.changeRoomSubject.asObservable();

	public get actualRoom() {
		return this.changeRoomSubject.getValue();
	}

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


		this.connection.start().then(async () => {
			const result = await this.connection.invoke<NavigationResult>('joinWorld');
			this.changeRoomSubject.next(result.roomId);
		});
	}

	async tryEnterRoom(direction: Direction) {

		const result = await this.connection.invoke<NavigationResult>('tryEnterRoom', direction);
		if (!result.succeeded) {
			this.receiveGameMessageSubject.next({ message: `Es befindet sich kein Raum im ${Direction[direction]}`, type: MessageType.Server});
		} else {
			this.changeRoomSubject.next(result.roomId);
		}

	}


	public sendUserInput(message: string) {
		this.receiveGameMessageSubject.next({ message, type: MessageType.Client });
	}

	public displayMessage(message: string) {
		this.receiveGameMessageSubject.next({ message, type: MessageType.Server });
	}

	public clearChat() {
		this.onClear.emit();
	}

}





