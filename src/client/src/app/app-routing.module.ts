import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChatComponent } from './chat/chat/chat.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { RoomsListComponent } from './rooms/rooms-list/rooms-list.component';
import { AppComponent } from './app.component';


const routes: Routes = [
	{
		path: '',
		canActivate: [ AuthGuard ],
		redirectTo: 'chat',
		pathMatch: 'full'
	},
	{
		path: 'login',
		component: LoginComponent
	},
	{
		path: 'chat',
		canActivate: [ AuthGuard ],
		children: [
			{
				path: '',
				component: ChatComponent
			}
		]
	},
	{
		path: 'rooms',
		canActivate: [ AuthGuard ],
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
