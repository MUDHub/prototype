import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

	loginForm: FormGroup;
	returnUrl: string;
	isLoading = false;
	isSubmitted = false;

	constructor(private formBuilder: FormBuilder, private auth: AuthService, private router: Router, private route: ActivatedRoute) { }

	ngOnInit(): void {
		this.loginForm = this.formBuilder.group({
			username: ['', Validators.required],
			password: ['', Validators.required]
		});

		this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
	}


	async onSubmit() {
		this.isSubmitted = true;
		if (this.loginForm.invalid) {
			return;
		}

		this.isLoading = true;

		try {
			await this.auth.login(this.loginForm.controls.username.value, this.loginForm.controls.password.value);
			await this.router.navigate([ this.returnUrl ]);
		} catch (err) {
			console.error(err.message, err);
			alert(err.message);
		} finally {
			this.isLoading = false;
		}
	}

}
