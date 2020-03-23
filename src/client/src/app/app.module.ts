import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { ChatModule } from './shell/chat/chat.module';
import { RoomsModule } from './shell/rooms/rooms.module';
import { ShellComponent } from './shell/shell.component';

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		ShellComponent,
	],
	imports: [
		BrowserModule,
		ChatModule,
		RoomsModule,
		FormsModule,
		ReactiveFormsModule,
		HttpClientModule,
		AppRoutingModule
	],
	providers: [
		{
			provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true
		}
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
