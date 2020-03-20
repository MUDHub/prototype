import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

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


	login(username: string, password: string) {

		return this.http.post('http://localhost:5000/users/authenticate', {
			username,
			password
		}).toPromise();

		// return new Promise((resolve, reject) => {
		// 	setTimeout(() => {
		// 		this.token = 'test';
		// 		resolve(true);
		// 	}, 2000);
		// });
	}
}
