import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../services/user.service';
import { environment as env } from 'src/environments/environment';

@Component({
	selector: 'app-chat',
	templateUrl: './chat.component.html',
	styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

	constructor(private http: HttpClient, private user: UserService) { }

	ngOnInit(): void {

	}


	sendDummy() {
		this.http.get(env.url + 'api/rooms', {
			headers: {
				Authorization: 'Bearer ' + this.user.getToken()
			}
		}).subscribe(data => console.log(data));
	}
}
