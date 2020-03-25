import { Component, OnInit } from '@angular/core';
import { GlobalChatService } from '../services/global-chat.service';

@Component({
	selector: 'app-global-chat',
	templateUrl: './global-chat.component.html',
	styleUrls: ['./global-chat.component.scss']
})
export class GlobalChatComponent implements OnInit {

	private publicMessageSubscription;
	private privateMessageSubscription;

	constructor(private chat: GlobalChatService) { }

	ngOnInit() {
		this.publicMessageSubscription = this.chat.newGlobalMessage$.subscribe(m => {
			console.log('public', m);
		});

		this.privateMessageSubscription = this.chat.newPrivateMessage$.subscribe(m => {
			console.log('private', m);
		});
	}



	send(input: HTMLInputElement) {
		console.log('sending', input.value);
		input.value = '';
		this.chat.sendGlobalMessage(input.value);
	}

}
