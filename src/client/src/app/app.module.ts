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
import { GameChatComponent } from './game/game-chat/game-chat.component';
import { InfoBarComponent } from './game/info-bar/info-bar.component';
import { MapComponent } from './game/info-bar/map/map.component';
import { InteractionComponent } from './game/info-bar/interaction/interaction.component';

@NgModule({
	declarations: [
		AppComponent,
		LoginComponent,
		ContainerComponent,
		GlobalChatComponent,
		GameComponent,
		GameChatComponent,
		InfoBarComponent,
		MapComponent,
		InteractionComponent,
	],
	imports: [
		BrowserModule,
		FormsModule,
		ReactiveFormsModule,
		HttpClientModule,
		AppRoutingModule,
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule { }
