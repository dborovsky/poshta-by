import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Upload {
    upload(formData): Observable<any>;
}