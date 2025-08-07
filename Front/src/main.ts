import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { authInterceptor } from './app/core/interceptor/auth.interceptor';
import { provideNzConfig, NzConfig } from 'ng-zorro-antd/core/config';

const ngZorroConfig: NzConfig = {
  form: {
    nzNoColon: true
  }
};
bootstrapApplication(AppComponent,{
  providers: [
    provideHttpClient(withInterceptors([authInterceptor])),
    provideRouter(routes),
    provideAnimations(),
    provideNzConfig(ngZorroConfig)
  ]
})
  .catch((err) => console.error(err));

