import { Injectable } from '@angular/core';
import signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatServiceService {

  connection: signalR.HubConnection;

  constructor() {
    this.connection = new signalR.HubConnectionBuilder().withUrl('/chat').build();

    this.connection.on('displayMessage', message => {
      console.log(message);
    });
  }
}
