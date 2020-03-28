import { Component, OnInit, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { GlobalChatService } from '../services/global-chat.service';
import { AuthService } from '../services/auth.service';
import { Subscription } from 'rxjs';
import { IMessage } from '../model/IMessage';

@Component({
	selector: 'app-global-chat',
	templateUrl: './global-chat.component.html',
	styleUrls: ['./global-chat.component.scss']
})
export class GlobalChatComponent implements OnInit, OnDestroy {

	private publicMessageSubscription: Subscription;
	private privateMessageSubscription: Subscription;


	private _errorMessage: string;
	get error() {
		return this._errorMessage;
	}
	set error(value: string) {
		this._errorMessage = value;

		if (value) {
			setTimeout(() => this.error = undefined, 4000);
		}
	}

	history: IMessage[] = [];

	username: string;


	@ViewChild('chat')
	private chatEl: ElementRef;


	constructor(private chat: GlobalChatService, private auth: AuthService) { }


	private addMessage(message: IMessage) {
		this.history.push(message);
		setTimeout(() => this.scrollToBottom(), 0);
		if (this.history.length > 500) {
			this.history.shift();
		}
	}


	ngOnInit() {
		this.publicMessageSubscription = this.chat.newGlobalMessage$.subscribe(m => {
			this.addMessage({
				message: m.message,
				name: m.name,
				private: false
			});
		});

		this.privateMessageSubscription = this.chat.newPrivateMessage$.subscribe(m => {
			this.addMessage({
				message: m.message,
				name: m.name,
				private: true
			});
		});


		this.username = this.auth.user.username;
	}

	ngOnDestroy() {
		if (this.privateMessageSubscription && !this.privateMessageSubscription.closed) {
			this.privateMessageSubscription.unsubscribe();
		}
		if (this.publicMessageSubscription && !this.publicMessageSubscription.closed) {
			this.publicMessageSubscription.unsubscribe();
		}
	}



	scrollToBottom() {
		this.chatEl.nativeElement.scrollTo(0, this.chatEl.nativeElement.scrollHeight);
	}


	async send(input: HTMLInputElement) {
		const message = input.value;

		if (message.startsWith('/')) {
			const command = message.split(' ')[0];
			const args = message.split(command)[1].trim() || undefined;
			try {
				await this.executeCommand(command, args);

				input.value = '';
			} catch (err) {
				console.error(err);
			}
		} else {
			if (this.chat.isConnected) {
				try {
					await this.chat.sendGlobalMessage(message);

					input.value = '';
				} catch (err) {
					this.error = err;
					console.error(err);

					setTimeout(() => {
						this.error = undefined;
					}, 800);
				}
			} else {
				this.error = 'Keine Verbindung zum Server';
			}
		}
	}


	async executeCommand(command: string, args: string) {
		try {
			switch (command.replace('/', '')) {
				case 'whisper':
					await this.whisper(args);
					break;

				case 'clear':
					this.history = [];
					break;

				default:
				throw new Error(`Unbekannter Befehl: "${command}"`);
			}
		} catch (err) {
			console.error(err);
			this.error = err.message;
		}
	}

	async whisper(args) {
		const to = args?.split(' ')[0];
		const message = args?.split(to)[1].trim() || undefined;

		if (to && message) {
			console.log(`sending private message to '${to}': "${message}"`);
			await this.chat.sendPrivateMessage(message, to);
			this.history.push({
				message,
				private: true
			});
		} else {
			throw new Error('Ungültiger Befehlsaufruf "/whisper <user> <message>"');
		}
	}

}
