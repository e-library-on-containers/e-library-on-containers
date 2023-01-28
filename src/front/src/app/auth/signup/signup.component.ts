import {Component} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {AuthService} from "../auth/auth.service";
import {filter} from "rxjs";

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  loginForm: FormGroup;
  submitted = false;
  invalidLogin = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService) {
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.email],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  get form() {
    return this.loginForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    // perform your login validation here and navigate to the appropriate page
    this.authService.createAccount({
      email: this.form['email'].value,
      firstName: this.form['firstname'].value,
      lastName: this.form['lastname'].value,
      password: this.form['password'].value,
    })
      .pipe(filter(result => result))
      .subscribe(
        () => this.router.navigate(['/']).then(() => console.log('Account created!'))
      )
  }
}
