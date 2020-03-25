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
		name: string,
		message: string,
		public: boolean
	}[] = [];

	username: string;
	error: string;

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
				public: true
			});
			this.scrollToBottom();
		});

		this.privateMessageSubscription = this.chat.newPrivateMessage$.subscribe(m => {
			this.history.push({
				message: m.message,
				name: m.name,
				public: false
			});
			this.scrollToBottom();
		});


		this.username = this.auth.user.username;
	}


	scrollToBottom() {
		this.chatEl.nativeElement.scrollTo(0, this.chatEl.nativeElement.scrollHeight);
	}


	async send(input: HTMLInputElement) {
		if (this.chat.isConnected) {
			try {
				await this.chat.sendGlobalMessage(input.value);
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
