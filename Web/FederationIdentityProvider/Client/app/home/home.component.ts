import { Component, OnInit } from '@angular/core';

import { User } from '../_models/index';
import { UserService } from '../_services/index';
import { DefaultUrlSerializer } from '@angular/router';

@Component({
    moduleId: module.id,
    templateUrl: 'home.component.html'
})

export class HomeComponent implements OnInit {
    currentUser: User;
    //users: User[] = [];

    constructor(private userService: UserService) {
        this.currentUser = new User();
        this.currentUser.username = "user name";
        this.currentUser.firstName = "";
        this.currentUser.lastName = "";
        //this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    }

    ngOnInit() {
        //localStorage.removeItem('currentUser');
        this.loadCurrentUserDetails();
        //this.loadAllUsers();
    }

    //deleteUser(id: number) {
    //    this.userService.delete(id).subscribe(() => { this.loadAllUsers() });
    //}

    //private loadAllUsers() {
    //    this.userService.getAll().subscribe(users => { this.users = users; });
    //}

    private loadCurrentUserDetails() {
        this.userService.getCurrentUserDetails().subscribe(user =>
        {
			//this.currentUser = new User();
			this.currentUser.firstName = user.forename;
			this.currentUser.lastName = user.surname;
			this.currentUser.username = user.userName;
        });
    }
}