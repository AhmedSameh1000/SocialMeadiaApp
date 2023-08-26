import { Injectable } from '@angular/core';
import { async } from '@angular/core/testing';
import * as signalr from "@microsoft/signalr"
import { IHttpConnectionOptions } from '@microsoft/signalr';
@Injectable({
  providedIn: 'root'
})
export class SignalRService {



  constructor() { }
  HubConnection!:signalr.HubConnection
  startConnection=(token:any)=>{


    this.HubConnection=new signalr.HubConnectionBuilder()
    .withUrl("https://localhost:7268/muhub",{
      skipNegotiation:true,
      transport:signalr.HttpTransportType.WebSockets,
      accessTokenFactory:async ()=>token
    })
    .withAutomaticReconnect()
    .build();

    this.HubConnection.
    start()
    .then(()=>{
      console.log("hub connection started")
    }).catch(Error=>console.log("error while starting connection"+Error))
  }
  stopConnection() {
    return this.HubConnection.stop();
  }
}
