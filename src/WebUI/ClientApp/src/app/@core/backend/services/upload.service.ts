import { HttpClient, HttpEvent, HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Injectable, Optional } from '@angular/core';
import { Observable } from 'rxjs';
import { UploadApi } from '../api/upload.api';
import { Upload } from '../interfaces/upload';

@Injectable()
export class UploadService implements Upload  {
  constructor(private uploadApi: UploadApi) { }

  upload(formData) : Observable<any> {
    return this.uploadApi.uploadExcell(formData, {
      reportProgress: true,
      observe: 'events'
    });
  }

}



