import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';

@Component({
	selector: 'app-shell',
	templateUrl: './shell.component.html',
	styleUrls: ['./shell.component.scss']
})
export class ShellComponent {

	constructor(private auth: AuthService, private router: Router) { }


	logout() {
		this.auth.logout();
		this.router.navigate(['/login']);
	}

}
