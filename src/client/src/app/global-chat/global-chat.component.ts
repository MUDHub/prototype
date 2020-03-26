import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { GlobalChatService } from '../services/global-chat.service';
import { AuthService } from '../services/auth.service';

@Component({
	selector: 'app-global-chat',
	templateUrl: './global-chat.component.html',
	styleUrls: ['./global-chat.component.scss']
})
export class GlobalChatComponent implements OnInit {

	history: {
		name?: string,
		message: string,
		private: boolean
	}[] = [];

	username: string;


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

	@ViewChild('chat')
	private chatEl: ElementRef;

	private publicMessageSubscription;
	private privateMessageSubscription;

	constructor(private chat: GlobalChatService, private auth: AuthService) { }

	ngOnInit() {
		this.publicMessageSubscription = this.chat.newGlobalMessage$.subscribe(m => {
			this.history.push({
				message: m.message,
				name: m.name,
				private: false
			});
			this.scrollToBottom();
		});

		this.privateMessageSubscription = this.chat.newPrivateMessage$.subscribe(m => {
			this.history.push({
				message: m.message,
				name: m.name,
				private: true
			});
			this.scrollToBottom();
		});


		this.username = this.auth.user.username;
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
