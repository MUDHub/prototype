import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Direction } from '../model/Direction';
import { IRoom } from '../model/IRoom';

@Injectable({
	providedIn: 'root'
})
export class RoomService {

	constructor(private http: HttpClient) { }


	public getRooms() {
		return this.http.get<IRoom[]>(environment.url + 'api/rooms').toPromise();
	}

	public getAdjacentRooms(roomId: string) {
		return this.http.get<{ direction: string, room: IRoom }[]>(environment.url + `api/rooms/${roomId}/adjacent`).toPromise();
	}
}



