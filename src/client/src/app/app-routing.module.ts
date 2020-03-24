import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChatComponent } from './shell/chat/chat/chat.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { RoomsListComponent } from './shell/rooms/rooms-list/rooms-list.component';
import { AppComponent } from './app.component';
import { ShellComponent } from './shell/shell.component';
import { RoomsCreateComponent } from './shell/rooms/rooms-create/rooms-create.component';


const routes: Routes = [
	{
		path: '',
		component: ShellComponent,
		canActivate: [ AuthGuard ],
		children: [
			{
				path: '',
				pathMatch: 'full',
				redirectTo: 'chat'
			},
			{
				path: 'chat',
				children: [
					{
						path: '',
						component: ChatComponent
					}
				]
			},
			{
				path: 'rooms',
				children: [
					{
						path: '',
						redirectTo: 'create',
						pathMatch: 'full'
					},
					{
						path: 'list',
						component: RoomsListComponent
					},
					{
						path: 'create',
						component: RoomsCreateComponent
					},
				]
			},
		]
	},
	{
		path: 'login',
		component: LoginComponent
	},
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
