import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment as env } from 'src/environments/environment';

@Injectable({
	providedIn: 'root'
})
export class UserService {

	private token: string;
	get isLoggedIn() {
		return this.token !== undefined;
	}
	constructor(private http: HttpClient) { }



	getToken(): string {
		return this.token;
	}


	async login(username: string, password: string) {

		const response = await this.http.post(env.url + 'users/authenticate', {
			username,
			password
		}).toPromise();

		this.token = (response as any).token;

		// return new Promise((resolve, reject) => {
		// 	setTimeout(() => {
		// 		this.token = 'test';
		// 		resolve(true);
		// 	}, 2000);
		// });
	}
}
