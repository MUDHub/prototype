import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../services/user.service';

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
		this.http.get('http://localhost:5000/api/rooms', {
			headers: {
				Authorization: 'Bearer ' + this.user.getToken()
			}
		}).subscribe(data => console.log(data));
	}
}
