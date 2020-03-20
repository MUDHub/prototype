import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { environment as env } from 'src/environments/environment';

@Component({
	selector: 'app-chat',
	templateUrl: './chat.component.html',
	styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {

	constructor(private http: HttpClient, private user: AuthService) { }

	ngOnInit(): void {

	}


	sendDummy() {
		this.http.get(env.url + 'api/rooms').subscribe(data => console.log(data));
	}
}
