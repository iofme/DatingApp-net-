import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { take } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  hubUrl = environment.hubsUrl;
  private hubConnection?: HubConnection;
  private toastr = inject(ToastrService);
  private route = inject(Router);
  onlineUsers = signal<string[]>([]);

  createHubbConnection(user: User){
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'presence', {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build();

      this.hubConnection.start().catch(error => console.log(error));

      this.hubConnection.on('UserIsOnline', username => {
          this.onlineUsers.update(user => [...user, username]);
      });
      
      this.hubConnection.on('UserIsOfline', username => {
        this.onlineUsers.update(user => user.filter(x => x!== username));
      });

      this.hubConnection.on('GetOnlinteUsers', usernames => {
        this.onlineUsers.set(usernames)
      })

      this.hubConnection.on('NewMessageReceived', ({username, knownAs}) => {
        this.toastr.info(knownAs + ' has send you a new message!, Click me to see it')
        .onTap
        .pipe(take(1))
        .subscribe(() => this.route.navigateByUrl('/members/' + username + '?tab=Messages'))
      })
  }

  stopHubConnection(){
    if(this.hubConnection?.state === HubConnectionState.Connected){
      this.hubConnection.stop().catch(error => console.log(error))
    }
  }
}
