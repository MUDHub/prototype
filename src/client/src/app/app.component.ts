import { Component } from '@angular/core';
import { ChatService } from './services/chat.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent {
	title = 'client';

	constructor(private chat: ChatService) { }
}
