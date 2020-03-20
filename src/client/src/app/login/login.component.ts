import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

	isLoading = false;

	constructor(private user: UserService, private router: Router) { }

	ngOnInit(): void {
	}


	async login(username: string, password: string) {
		this.isLoading = true;
		try {
			await this.user.login(username, password);
			await this.router.navigate(['/']);
		} catch (err) {
			console.error('Login - ' + err.message, err);
			alert(err.message);
		} finally {
			this.isLoading = false;
		}
	}

}
