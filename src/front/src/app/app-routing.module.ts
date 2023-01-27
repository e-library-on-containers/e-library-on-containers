import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {PageNotFoundComponent} from "./page-not-found/page-not-found.component";
import {RentalsComponent} from "./books/rentals/rentals.component";
import {LibraryComponent} from "./books/library/library.component";
import {LoginComponent} from "./auth/login/login.component";
import {SignupComponent} from "./auth/signup/signup.component";

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'library', component: LibraryComponent},
  {path: 'rental', component: RentalsComponent},
  {path: 'signup', component: SignupComponent},
  {path: '', redirectTo: '/library', pathMatch: 'full'},
  {path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
