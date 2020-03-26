import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment as env } from 'src/environments/environment';


@Injectable({
	providedIn: 'root'
})
export class AuthService {

	private _token: string;
	private _user: any;


	get loggedIn() {
		return this._token !== undefined;
	}

	get token() {
		return this._token;
	}

	get user() {
		return this._user;
	}


	constructor(private http: HttpClient) {
		const sessionToken = sessionStorage.getItem('token');
		if (sessionToken) {
			this._token = sessionToken;
		}

		const sessionUser = sessionStorage.getItem('user');
		if (sessionUser) {
			this._user = JSON.parse(sessionUser);
		}
	}


	public async login(username: string, password: string) {
		try {
			const result = await this.http
				.post<{ succeeded: boolean, token?: string, user: any }>(env.url + 'users/login', { username, password }).toPromise();

			this._token = result.token;
			sessionStorage.setItem('token', this._token);

			this._user = result.user;
			sessionStorage.setItem('user', JSON.stringify(this._user));

			return Promise.resolve();
		} catch (err) {
			return Promise.reject(err);
		}
	}

}
