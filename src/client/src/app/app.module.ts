import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ContainerComponent } from './container/container.component';
import { GlobalChatComponent } from './global-chat/global-chat.component';
import { GameComponent } from './game/game.component';

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		ContainerComponent,
		GlobalChatComponent,
		GameComponent,
	],
	imports: [
		BrowserModule,
		ReactiveFormsModule,
		HttpClientModule,
		AppRoutingModule,
		FormsModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
