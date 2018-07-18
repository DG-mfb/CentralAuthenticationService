import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';

import { User } from '../_models/index';

@Injectable()
export class UserService {
   
    constructor(private http: Http) { }

    getCurrentUserDetails() {
        return this.http.get('https://localhost:44397/api/account/user', this.jwt()).map((response: Response) => response.json());
    }

    getAll() {
        return this.http.get('/api/account/userDetails', this.jwt()).map((response: Response) => response.json());
    }

    getById(id: number) {
        return this.http.get('/api/users/' + id, this.jwt()).map((response: Response) => response.json());
    }

    create(user: User) {
        return this.http.post('/api/users', user, this.jwt()).map((response: Response) => response.json());
    }

    update(user: User) {
        return this.http.put('/api/users/' + user.id, user, this.jwt()).map((response: Response) => response.json());
    }

    delete(id: number) {
        return this.http.delete('/api/users/' + id, this.jwt()).map((response: Response) => response.json());
    }

    resetPassword(name: string) {
        const headers = new Headers();
        headers.append('Content-Type', 'application/json');
        let options = new RequestOptions({ headers: headers });
        var content = JSON.stringify(name);
        return this.http.post('/api/account/resetpassword', content, options).map((response: Response) => response.json());
    }

    updatePassword(password: string, confirmPassword: string, ticket: string) {
        const headers = new Headers();
        headers.append('Content-Type', 'application/json');
        let options = new RequestOptions({ headers: headers });
        var content = JSON.stringify({ Name: name, Password: password, ConfirmPassword: confirmPassword, Ticket: ticket });
        return this.http.post('/api/account/updatepassword', content, options).map((response: Response) => response.json());
    }


    private jwt() {
        // create authorization header with jwt token
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.access_token) {
            let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.access_token });
            //headers.append("Access-Control-Allow-Origin", "*");
            return new RequestOptions({ headers: headers });
        }
    }
}