import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  NbActionsModule,
  NbButtonModule,
  NbCardModule,
  NbTabsetModule,
  NbIconModule,
  NbListModule,
  NbRadioModule,
  NbSelectModule,
  NbUserModule,
  NbProgressBarModule
} from '@nebular/theme';
import { ConvertComponent } from './convert.component';
import { NgxEchartsModule } from 'ngx-echarts';
import { ThemeModule } from '../../@theme/theme.module';

@NgModule({
  imports: [
    NbTabsetModule,
    ThemeModule,
    NbCardModule,
    NbUserModule,
    NbButtonModule,
    NbTabsetModule,
    NbActionsModule,
    NbRadioModule,
    NbSelectModule,
    NbListModule,
    NbIconModule,
    NbButtonModule,
    NgxEchartsModule,
    BrowserAnimationsModule,
    NbProgressBarModule
  ],
  declarations: [
    ConvertComponent
  ],
})
export class ConvertModule { }
