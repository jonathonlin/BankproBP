import { PermissionService } from './../../services/api.client.generated';
import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, Input, OnInit } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { map } from 'rxjs/operators';
import { ProgramNode } from 'src/app/models/program-node';
import { TreeNode } from 'src/app/models/tree-node';
import { CompanyProgramReadDTO } from 'src/app/services/api.client.generated';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent implements OnInit {  
  @Input() sideNav: MatSidenav;

  private _transformer = (node: ProgramNode, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.programName,
      url: node.programUrl,
      level: level,
    };
  }

  treeControl = new FlatTreeControl<TreeNode>(
    node => node.level, node => node.expandable);

  treeFlattener = new MatTreeFlattener(
    this._transformer, node => node.level, node => node.expandable, node => node.children);

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  
  constructor(    
    private permissionService: PermissionService,
    ) { 
      this.permissionService.getRolePermission()
        .pipe(
          map(m => {
            let data = m.sort((a,b) => a.sort - b.sort);
            let res = this.buildMenu(null, data);
            this.dataSource.data = res;         
            // this.treeControl.expandAll();
          })            
        ).subscribe(res => this.sideNav.toggle());    
  }

  hasChild = (_:number, node: TreeNode) => node.expandable;

  ngOnInit(): void {    
    
  }

  buildMenu(id:any, data: CompanyProgramReadDTO[]){    
    let res = data.filter(f=>f.parentId === id);      
    return res.map(m => ({
      ...m,
      children: this.buildMenu(m.id, data)
    }));   
  } 
}
