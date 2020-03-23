import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoomsListComponent } from './rooms-list/rooms-list.component';
import { RoomsCreateComponent } from './rooms-create/rooms-create.component';
import { RoomComponent } from './rooms-create/room/room.component';



@NgModule({
	declarations: [RoomsListComponent, RoomsCreateComponent, RoomComponent],
	imports: [
		CommonModule
	]
})
export class RoomsModule { }
