import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';


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
    RouterModule.forRoot([
      /*{ path: '', component: HomeComponent, pathMatch: 'full' },*/
      { path: 'login', component: LoginComponent, data: { title: 'Login' } },
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: DemoComponent },
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

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
