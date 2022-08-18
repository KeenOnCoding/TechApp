import { BrowserModule } from '@angular/platform-browser';
import { Injectable, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { DefaultUrlSerializer, RouterModule, UrlSerializer, UrlTree } from '@angular/router';


import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DemoComponent } from './components/demo/demo.component';
import { NgxSpinnerModule } from "ngx-spinner";
import { NgxImgZoomModule } from 'ngx-img-zoom';
import { MainComponent } from './components/main/main.component';
import { ShopModule } from './components/shop/shop.module';
import { SharedModule } from './components/shared/shared.module';
import { LoginComponent } from './login/login.component';


import { OAuthModule } from 'angular-oauth2-oidc';


import { AppTitleService } from './services/app-title.service';

import { ConfigurationService } from './services/configuration.service';
import { AlertService } from './services/alert.service';
import { ThemeManager } from './services/theme-manager';
import { LocalStoreManager } from './services/local-store-manager.service';
import { OidcHelperService } from './services/oidc-helper.service';
import { NotificationService } from './services/notification.service';
import { NotificationEndpoint } from './services/notification-endpoint.service';
import { AccountService } from './services/account.service';
import { AccountEndpoint } from './services/account-endpoint.service';
import { AuthGuard } from './services/auth-guard.service';
import { AuthService } from './services/auth.service';
import { Utilities } from './services/utilities';
@Injectable()
export class LowerCaseUrlSerializer extends DefaultUrlSerializer {
  parse(url: string): UrlTree {
    const possibleSeparators = /[?;#]/;
    const indexOfSeparator = url.search(possibleSeparators);
    let processedUrl: string;

    if (indexOfSeparator > -1) {
      const separator = url.charAt(indexOfSeparator);
      const urlParts = Utilities.splitInTwo(url, separator);
      urlParts.firstPart = urlParts.firstPart.toLowerCase();

      processedUrl = urlParts.firstPart + separator + urlParts.secondPart;
    } else {
      processedUrl = url.toLowerCase();
    }

    return super.parse(processedUrl);
  }
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    DemoComponent,
    MainComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgxSpinnerModule,
    BrowserModule,
    SharedModule,
    ShopModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    NgxImgZoomModule,
    OAuthModule.forRoot(),
    RouterModule.forRoot([
      /*{ path: '', component: HomeComponent, pathMatch: 'full' },*/
      { path: 'login', component: LoginComponent, data: { title: 'Login' } },
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: DemoComponent, canActivate: [AuthGuard] },
      { path: '', component: MainComponent, children: [
          {
            path: 'home',
            loadChildren: () => import('./components/shop/shop.module').then(m => m.ShopModule)
          },
          {
            path: 'pages',
            loadChildren: () => import('./components/pages/pages.module').then(m => m.PagesModule)

          },
          {
            path: 'blog',
            loadChildren: () => import('./components/blog/blog.module').then(m => m.BlogModule)
          },
        ] },
      { path: '**', redirectTo: 'home/one' }])
  ],
  providers: [
    AlertService,
    ThemeManager,
    ConfigurationService,
    AppTitleService,
    NotificationService,
    NotificationEndpoint,
    AccountService,
    AccountEndpoint,
    LocalStoreManager,
    OidcHelperService,
    AuthService,
    AuthGuard,
    { provide: UrlSerializer, useClass: LowerCaseUrlSerializer }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }



