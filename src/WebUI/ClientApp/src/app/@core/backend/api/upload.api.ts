import { Injectable, Optional } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';

@Injectable()
export class UploadApi {
  private readonly apiController: string = 'upload';

  constructor(private api: HttpService) { }

  uploadExcell(data, options?): Observable<any> {
    return this.api.post(this.apiController, data, options);
  }
}