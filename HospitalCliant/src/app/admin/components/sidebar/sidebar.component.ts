import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IUserTokenProvider } from 'src/app/core/models/UserTokenProvider';
import { LoaderService } from 'src/app/Shared/loader/loader.service';

declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}

export const ROUTES: RouteInfo[] = [
  { path: '/admin/dashboard', title: 'Dashboard',  icon: 'dashboard', class: 'router-link-active' },
  { path: '/admin/user/userlist', title: 'Users',  icon: 'person', class: 'router-link-active' },
  { path: '/admin/hospital', title: 'Hospital',  icon: 'person', class: 'router-link-active' },
  { path: '/admin/membershipbranch', title: 'Branch',  icon: 'person', class: 'router-link-active' },
  { path: '/admin/medicine', title: 'Medicine',  icon: 'person', class: 'active' },
];

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];
  currentUser$: Observable<IUserTokenProvider>;
  constructor(private toastr: ToastrService,
              private accountService: AccountService,
              public loaderService: LoaderService) { }

  ngOnInit(): void {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
    this.currentUser$ = this.accountService.currentUser$;
  }

  logout() {
    this.accountService.logout();
    this.toastr.success('Successfully Logout');
  }

}
