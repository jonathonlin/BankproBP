import { TreeNode } from './../../../../models/tree-node';
import { ProgramNode } from './../../../../models/program-node';
import { PaginationResponseOfProgramRoleReadDTO, ProgramRoleService, CompanyProgramService, CompanyProgramReadDTO, PermissionService } from './../../../../services/api.client.generated';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { map } from 'rxjs/operators';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';
import { SelectionModel } from '@angular/cdk/collections';



@Component({
  selector: 'app-program-setting',
  templateUrl: './program-setting.component.html',
  styleUrls: ['./program-setting.component.css']
})
export class ProgramSettingComponent implements OnInit {
  title: string = "權限維護";  
  roleOptions: {text: string, value: any}[] = [];
  displayColumns: string[] = ['name', 'id']

  errorMessage?: string;
  success: boolean = false;
  
  roleId: number;
  progarms: any;

  flatNodeMap = new Map<TreeNode, ProgramNode>();
  nestedNodeMap = new Map<ProgramNode, TreeNode>();
  selectedParent: TreeNode | null = null;
  treeControl: FlatTreeControl<TreeNode>;
  treeFlattener: MatTreeFlattener<ProgramNode, TreeNode>;
  dataSource: MatTreeFlatDataSource<ProgramNode, TreeNode>;
  checklistSelection = new SelectionModel<TreeNode>(true);

  @Output() reloadSidenav: EventEmitter<any> = new EventEmitter()

  constructor(    
    private programRoleService: ProgramRoleService,    
    private companyProgramService: CompanyProgramService,
    private permissionService: PermissionService,
  ) { 
    
    this.treeControl = new FlatTreeControl<TreeNode>(
      node => node.level, node => node.expandable);
    this.treeFlattener = new MatTreeFlattener(
        this.transformer, this.getLevel, node => node.expandable, node => node.children);
     

     this.companyProgramService.getAll(undefined,undefined,undefined,undefined,undefined,undefined,undefined)
     .pipe(
        map(m => {                
          let data = m.data.sort((a,b) => a.sort - b.sort);          
          let res = this.buildMenu(null, data);
          this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
          this.dataSource.data = res;       
          this.treeControl.expandAll();                
       })        
     ).subscribe();   
  }

  getLevel = (node: TreeNode) => node.level;
  isExpandable = (node: TreeNode) => node.expandable;
  getChildren = (node: ProgramNode) => node.children;
  hasChild = (_: number, _nodeData: TreeNode) => _nodeData.expandable;

  transformer = (node: ProgramNode, level: number) => {
    const existingNode = this.nestedNodeMap.get(node);
    const flatNode = existingNode && existingNode.name === node.programName
      ? existingNode : new TreeNode();
      flatNode.id = node.id;
    flatNode.name = node.programName;
    flatNode.url = node.programUrl;
    flatNode.level = level;
    flatNode.expandable = !!node.children.length;
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node,flatNode);
    return flatNode;
  }

  /** Whether all the descendants of the node are selected. */
  descendantsAllSelected(node: TreeNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected = descendants.length > 0 && descendants.every(child => {
      return this.checklistSelection.isSelected(child);
    });
    return descAllSelected;
  }

  descendantsPartiallySelected(node: TreeNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const result = descendants.some(child => this.checklistSelection.isSelected(child));
    return result && !this.descendantsAllSelected(node);
  }

  todoItemSelectionToggle(node: TreeNode): void {    
    this.checklistSelection.toggle(node);
    const descendants = this.treeControl.getDescendants(node); 
    
    this.checklistSelection.isSelected(node)
      ? this.checklistSelection.select(...descendants)
      : this.checklistSelection.deselect(...descendants);
      
    // Force update for the parent
    descendants.forEach(child => this.checklistSelection.isSelected(child));
    this.checkAllParentsSelection(node);
  }

  /** Toggle a leaf to-do item selection. Check all the parents to see if they changed */
  todoLeafItemSelectionToggle(node: TreeNode): void {
    this.checklistSelection.toggle(node);
    this.checkAllParentsSelection(node);
  }

  checkAllParentsSelection(node: TreeNode): void {
    let parent: TreeNode | null = this.getParentNode(node);
    while (parent) {
      this.checkRootNodeSelection(parent);
      parent = this.getParentNode(parent);
    }
  }

  /** Check root node checked state and change it accordingly */
  checkRootNodeSelection(node: TreeNode): void {
    const nodeSelected = this.checklistSelection.isSelected(node);
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected = descendants.length > 0 && descendants.some(child => {
      return this.checklistSelection.isSelected(child);
    });
    if (nodeSelected && !descAllSelected) {
      this.checklistSelection.deselect(node);
    } else if (!nodeSelected && descAllSelected) {
      this.checklistSelection.select(node);
    }
  }

  /* Get the parent node of a node */
  getParentNode(node: TreeNode): TreeNode | null {
    const currentLevel = this.getLevel(node);

    if (currentLevel < 1) {
      return null;
    }

    const startIndex = this.treeControl.dataNodes.indexOf(node) - 1;

    for (let i = startIndex; i >= 0; i--) {
      const currentNode = this.treeControl.dataNodes[i];

      if (this.getLevel(currentNode) < currentLevel) {
        return currentNode;
      }
    }
    return null;
  }
  
  ngOnInit(): void {
    this.programRoleService.getAll(undefined,undefined,undefined,undefined,undefined,undefined)
      .pipe(
        map((m: PaginationResponseOfProgramRoleReadDTO) => {
          let data = m.data.map(x=>({ text: x.roleName, value: x.id}));
          data.unshift({text:'請選擇', value: undefined});
          this.roleOptions = data;
        })
      ).subscribe();    
  }

  
  onSelectChange(){
    this.success = false;
    if(this.roleId === undefined) {
      this.treeControl.dataNodes.forEach(e => this.checklistSelection.deselect(e));
      return;
    }
    this.permissionService.get(this.roleId).subscribe(res=>{     
      this.treeControl.dataNodes.forEach(e => {        
        if(res.findIndex(f=>f.programId === e.id) > -1){
          this.checklistSelection.select(e);
        } else {
          this.checklistSelection.deselect(e);
        }
      });
    });
  } 

 
  buildMenu(id:any, data: CompanyProgramReadDTO[]){    
    let res = data.filter(f=>f.parentId === id);      
    return res.map(m => ({
      ...m,
      children: this.buildMenu(m.id, data)
    }));   
  }

  onSave() {
    if(this.roleId === undefined){
      this.errorMessage = "請選擇角色";
      return;
    }
    if(this.checklistSelection.selected.length === 0){
      this.errorMessage = "請選擇程式";
      return;
    }    


    const data: any[] = this.checklistSelection.selected
      .map(m => ({ roleId: this.roleId, programId: m.id}));
        

    this.permissionService.post(data).subscribe(res => {
      if(res.isOk){
        this.success = true;
        this.errorMessage = undefined;
        this.reloadSidenav.emit();
      } else {
        this.errorMessage = res.message;
      }
    });
  }
}
