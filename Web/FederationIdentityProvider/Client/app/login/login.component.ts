import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AlertService, AuthenticationService } from '../_services/index';
import { UserService } from "../_services/user.service";
import { User } from "../_models/user";
import { DOCUMENT } from '@angular/platform-browser';

@Component({
    moduleId: module.id,
    templateUrl: 'login.component.html'
})

export class LoginComponent implements OnInit {
    model: any = {};
    loading = false;
	returnUrl: string;
	state: string;
    token: string;
	constructor( @Inject(DOCUMENT) private document: any,
		private route: ActivatedRoute,
		private userService: UserService,
        private router: Router,
        private authenticationService: AuthenticationService,
        private alertService: AlertService) { }

    ngOnInit() {
        var currentUser = localStorage.getItem("currentUser");
        if (currentUser) {
            localStorage.removeItem('currentUser');
            //this.document.location.href = 'https://localhost:44382/api/account/logout'
        }
        this.token = this.route.snapshot.queryParams['token'];
        if (this.token) {

            localStorage.setItem("currentUser", JSON.stringify({ access_token: this.token }));
            this.router.navigate(['']);
        }
        // reset login status
		//this.authenticationService.logout();
        // get return url from route parameters or default to '/'
		this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
		this.state = this.route.snapshot.queryParams['state'] || '/';
    }

    login() {
        this.loading = true;
		this.authenticationService.login(this.model.username, this.model.password)
			.subscribe(
			data => {
				this.authenticationService.ssologin(data.access_token, this.returnUrl, this.state)
					.subscribe(
					d => {
						//this.document.location.href = 'http://localhost:60879/api/Account/SSOLogon';
						this.document.location.href = this.returnUrl + "?state=" + this.state;
						//this.router.navigate(['this.returnUrl']);
					})
				//this.router.navigate([this.returnUrl]);
			},
			error => {
				this.alertService.error(error);
				this.loading = false;
			});
    }
    sso() {
        this.document.location.href = 'https://localhost:44316/account/sso?clientId=testshib';
    }
}