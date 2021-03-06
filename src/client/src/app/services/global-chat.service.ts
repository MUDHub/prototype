import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { environment as env } from 'src/environments/environment';
import { Subject } from 'rxjs';
import { AuthService } from './auth.service';
import { HttpTransportType } from '@aspnet/signalr';

@Injectable({
	providedIn: 'root'
})
export class GlobalChatService {

	private newGlobalMessageSubject = new Subject<{ message: string, name: string }>();
	public newGlobalMessage$ = this.newGlobalMessageSubject.asObservable();

	private newPrivateMessageSubject = new Subject<{ message: string, name: string }>();
	public newPrivateMessage$ = this.newPrivateMessageSubject.asObservable();

	get isConnected() {
		return this.connection.state === signalR.HubConnectionState.Connected;
	}

	private connection: signalR.HubConnection;

	constructor(private auth: AuthService) {
		this.connection = new signalR.HubConnectionBuilder()
			.withUrl(env.url + 'hubs/chat', {
				skipNegotiation: true,
				transport: HttpTransportType.WebSockets,
				accessTokenFactory: () => this.auth.token
			})
			.build();


		this.connection.on('receiveGlobalMessage', (message, name) => {
			this.newGlobalMessageSubject.next({ message, name });
		});

		this.connection.on('receivePrivateMessage', (message, name) => {
			this.newPrivateMessageSubject.next({ message, name });
		});


		this.connection.start();
	}


	public sendGlobalMessage(message: string) {
		return this.connection.invoke('sendGlobalMessage', message);
	}

	public sendPrivateMessage(message: string, username: string) {
		return this.connection.invoke('sendPrivateMessage', message, username);
	}


}
