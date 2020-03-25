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


	constructor(private http: HttpClient) { }


	public async login(username: string, password: string) {
		try {
			const result = await this.http
				.post<{ succeeded: boolean, token?: string, user: any }>(env.url + 'users/login', { username, password }).toPromise();

			this._token = result.token;
			this._user = result.user;

			return Promise.resolve();
		} catch (err) {
			return Promise.reject(err);
		}
	}

}
