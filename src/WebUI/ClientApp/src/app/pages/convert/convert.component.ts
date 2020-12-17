import { Component, OnInit, ViewChild, ElementRef  } from '@angular/core';
import { HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { of } from 'rxjs';  
import { catchError, map } from 'rxjs/operators';  
import { UploadService } from  '../../@core/backend/services/upload.service';


@Component({
  selector: 'ngx-convert-elements',
  styleUrls: ['convert.component.scss'],
  templateUrl: './convert.component.html',
  providers: [UploadService]
})
export class ConvertComponent {

    @ViewChild("fileUpload", {static: false}) fileUpload: ElementRef;
    file = { data: File, inProgress: false, progress: 0};

    constructor(private uploadService: UploadService) {}

    uploadFile(file) {
      const formData = new FormData();
      formData.append(file, file.data);
      file.inProgress = true;
      this.uploadService.upload(formData).pipe(
        map(event => {
          switch (event.type) {
            case HttpEventType.UploadProgress:
              file.progress = Math.round(event.loaded * 100 / event.total);
              break;
            case HttpEventType.Response:
              return event;  
          }
        }),
        catchError((error: HttpErrorResponse) => {
          file.inProgress = false;
          return of(`${file.data.name} upload failed`);
        })).subscribe((event: any) => {
          if(typeof (event)=== 'object') {
            console.log(event.body);
          }
        });
    }

    onClick() {
      const fileUpload = this.fileUpload.nativeElement;
      //fileUpload.onchange = () => {
        this.file = { data: fileUpload.files[0], inProgress: false, progress: 0 };
        this.uploadFile(this.file);
      //};
      //fileUpload.click(); 
    }
}
