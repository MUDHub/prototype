import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ContainerComponent } from './container/container.component';
import { AuthGuard } from './guards/auth.guard';


const routes: Routes = [
	{
		path: 'login',
		component: LoginComponent
	}, {
		path: '',
		component: ContainerComponent,
		canActivate: [ AuthGuard ]
	}
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
