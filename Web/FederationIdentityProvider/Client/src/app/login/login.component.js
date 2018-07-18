"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var AuthenticationService_1 = require("../services/AuthenticationService");
var platform_browser_1 = require("@angular/platform-browser");
var LoginComponent = (function () {
    function LoginComponent(document, route, router, authenticationService) {
        this.document = document;
        this.route = route;
        this.router = router;
        this.authenticationService = authenticationService;
        this.showWarningMessage = false;
        this.showErrorMessage = false;
        this.loading = false;
        this.model = {};
    }
    LoginComponent.prototype.ngOnInit = function () {
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    };
    LoginComponent.prototype.login = function () {
        var _this = this;
        this.clearMessages();
        var req = this.authenticationService.login(this.model.username, this.model.password);
        req.subscribe(function (data) {
            _this.router.navigate([_this.returnUrl]);
        }, function (error) {
            //this.alertService.error(error);
            _this.loading = false;
        });
        //this.userService.currentUser$
        //    .subscribe(user => {
        //        if (user != null) {
        //            this.clearMessages();
        //            this.router.navigate(['/']);   
        //        }
        //        else {
        //            this.showErrorMessage = false;
        //            this.showWarningMessage = true;
        //        }                    
        //    },
        //    error => {
        //        this.showErrorMessage = true;
        //        this.showWarningMessage = false;
        //    });
    };
    LoginComponent.prototype.sso = function () {
        this.document.location.href = '"https://localhost:44316/account/sso?clientId=testshib&&redirectUrl=https://localhost:44342/';
    };
    LoginComponent.prototype.clearMessages = function () {
        this.showWarningMessage = false;
        this.showErrorMessage = false;
    };
    return LoginComponent;
}());
LoginComponent = __decorate([
    core_1.Component({
        moduleId: module.id,
        selector: 'login',
        templateUrl: 'login.component.html',
        providers: [AuthenticationService_1.AuthenticationService],
        styleUrls: ['login.component.css']
    }),
    __param(0, core_1.Inject(platform_browser_1.DOCUMENT)),
    __metadata("design:paramtypes", [Object, router_1.ActivatedRoute,
        router_1.Router,
        AuthenticationService_1.AuthenticationService])
], LoginComponent);
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map