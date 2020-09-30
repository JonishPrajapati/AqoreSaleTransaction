import { Component, OnInit } from '@angular/core';
import { MvUserDetail } from './user-detail';
import { UserDetailService } from './user-detail.service';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators'


@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent implements OnInit {
  
  errorMessage: string = null;
  displayedColumns: string[] ;
  dataSource: MvUserDetail[]=[];
  

  constructor(private router: ActivatedRoute,
               private userDetails: UserDetailService) {
                }

  ngOnInit():void {
      this.displayedColumns = ['UserId', 'UserName','Password','InsertPersonId','InsertDate','FirstName', 'LastName'];
      this.getUserDetails();
  }
  getUserDetails() {
      const UserId = localStorage.getItem('UserId');
            // if user is null
            console.log(UserId,"id");
            
            if(UserId) {
              this.userDetails.getUserDetail(UserId).subscribe((data: any) => {
                console.log("datadata", data);
                
                this.dataSource = data.Details;
              })
            } else {
              this.dataSource = [];
              this.errorMessage = 'No data';
}
  }
  
  
  getAllUsers(){
    
    this.userDetails.getAllUser().subscribe((response: any)=>{
      console.log("hello");
        if(response && response.data){
          this.dataSource = response.data;
          console.log("res",this.dataSource);
          
        }else{
          this.errorMessage = "No Data Available"
        }
    })
  }


}
