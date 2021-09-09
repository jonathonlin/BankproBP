import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent implements OnInit {
  @Input() dataSource: any;
  @Input() initColumns!: {key:string, name:string, width?:string}[];  
  displayedColumns!: string[];
  pageEvent!: PageEvent;
  @Output() pageChange: EventEmitter<any> = new EventEmitter(); 
  @Output() edit: EventEmitter<any> = new EventEmitter();
  @Output() delete: EventEmitter<any> = new EventEmitter();
  @Output() sort: EventEmitter<any> = new EventEmitter();
  @ViewChild(MatPaginator) paginator?: MatPaginator;
  @ViewChild(MatSort) matSort?: MatSort;
  constructor() { }

  ngOnInit(): void {
    this.displayedColumns = this.initColumns.map(m=>m.key);
  }

  onPaginateChange(event:PageEvent){
    this.pageChange.emit(event);
  }

  onDelete(id: any){
    this.delete.emit(id);
  }

  onEdit(id: any){
    this.edit.emit(id);
  }

  sortData(event: Sort){
    this.sort.emit(event);    
  }
}
