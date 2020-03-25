import { Component, OnInit } from '@angular/core';
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
		});

		this.privateMessageSubscription = this.chat.newPrivateMessage$.subscribe(m => {
			this.history.push({
				message: m.message,
				name: m.name,
				public: false
			});
		});


		this.username = this.auth.user.username;
	}



	send(input: HTMLInputElement) {
		console.log('sending', input.value);
		this.chat.sendGlobalMessage(input.value);
		input.value = '';
	}

}
