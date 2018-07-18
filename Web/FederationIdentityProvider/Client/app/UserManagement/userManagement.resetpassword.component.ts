import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AlertService } from '../_services/index';
import { UserService } from "../_services/user.service";
import { User } from "../_models/user";
import { DOCUMENT } from '@angular/platform-browser';

@Component({
    moduleId: module.id,
    templateUrl: 'userManagement.resetpassword.component.html'
})

export class ResetPassword implements OnInit {
    model: any = {};
    loading = false;
	returnUrl: string;
    state: string;
    token: string;
	constructor( @Inject(DOCUMENT) private document: any,
		private route: ActivatedRoute,
		private userService: UserService,
        private router: Router,
        private alertService: AlertService) { }

    ngOnInit() {
        
    }

    updatepassword() {
        var token = this.route.snapshot.queryParams["resetpasswordticket"];
        this.loading = true;
        this.userService.updatePassword(this.model.password, this.model.confirmPassword, token)
            .subscribe(
            data => {
                this.router.navigate(['login']);
            },
            error => {
                this.alertService.error(error);
                this.loading = false;
            });
    }
}