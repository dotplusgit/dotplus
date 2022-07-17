import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IMedicine } from 'src/app/core/models/Medicine/medicine';
import { MedicineService } from '../medicine.service';


import { filter, distinctUntilChanged, debounceTime, tap, switchMap } from 'rxjs/operators';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-medicine-list',
  templateUrl: './medicine-list.component.html',
  styleUrls: ['./medicine-list.component.css']
})
export class MedicineListComponent implements OnInit , AfterViewInit {
  displayedColumns: string[] = ['BrandName', 'GenericName', 'Manufacturar',
                                 'Unit', 'UnitPrice', 'Active', 'Edit/View'];
  medicines: IMedicine[] = [];
  title = 'Medicine List';
  footerName = 'Data';
  dataSource = new MatTableDataSource<IMedicine>(this.medicines);
  @ViewChild(MatSort, {static: false}) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  isAdmin$: Observable<boolean>;
  isDoctor$: Observable<boolean>;

  minLengthTerm = 3;
  patientsearch = new FormControl();

  constructor(private medicineService: MedicineService, private accountService: AccountService) { }
  ngOnInit(): void {
    this.isAdmin$ = this.accountService.isAdmin$;
    this.isDoctor$ = this.accountService.isdesignationDoctor$;
    this.getMedicineList();
    this.dataSource.paginator = this.paginator;
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  getMedicineList(){
    this.medicineService.getAllMedicine().subscribe(response => {
      this.medicines = response;
      this.dataSource.data = response;
    }, error => {
      console.log(error);
    });
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
