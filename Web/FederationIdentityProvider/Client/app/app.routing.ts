import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/index';
import { LoginComponent } from './login/index';
import { RegisterComponent } from './register/index';
import { AuthGuard } from './_guards/index';
import { UserManagement } from './UserManagement/userManagement.component';
//import { ResetPassword } from './UserManagement';
//import { UserManagement, ResetPassword } from './UserManagement';

const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'usermanagement', component: UserManagement },
    //{ path: 'resetpassword', component: ResetPassword },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);