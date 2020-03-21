import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChatComponent } from './chat/chat.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { RoomsListComponent } from './rooms/rooms-list/rooms-list.component';


const routes: Routes = [
	{
		path: '',
		component: ChatComponent,
		canActivate: [AuthGuard]
	},
	{
		path: 'login',
		component: LoginComponent
	},
	{
		path: 'rooms',
		canActivate: [AuthGuard],
		children: [
			{
				path: '',
				component: RoomsListComponent
			}
		]
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
