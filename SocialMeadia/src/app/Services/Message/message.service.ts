import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { Message } from 'src/app/Models/message';
import { paginationResult } from 'src/app/Models/pagination';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private Http:HttpClient) { }
  GetMessages(id:string,page?:number,itemsPerPage?:number,MessageType?:any):Observable<paginationResult<Message[]>>{
    const _paginationResult:paginationResult<Message[]>=new paginationResult<Message[]>();
    let params=new HttpParams();
    params=params.append("MessageType",MessageType);
    if(page!=null&&itemsPerPage!=null){
      params= params.append("pageNumber",page);
      params= params.append("pageSize",itemsPerPage);
    }
    return this.Http.get<paginationResult<Message[]>>(environment.BaseApi+"api/Users/"+id+"/Messages",{observe:"response",params})
    .pipe(
      map((res:any)=>{
        _paginationResult.result=res.body;
        if(res.headers.get("pagination")!==null){
          _paginationResult.pagination=JSON.parse(res.headers.get("pagination"))
        }
        return _paginationResult;
      })
    );
  }

  GetConversation(id:string,recipientId:string):Observable<Message[]>{
    return this.Http.get<Message[]>(environment.BaseApi+"api/Users/"+id+"/Messages/Chat/"+recipientId);
  }
  SendMessage(id:any,Message:Message):Observable<Message>{
    return this.Http.post<Message>(`${environment.BaseApi}api/Users/${id}/Messages`,Message);
  }
}
