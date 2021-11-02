import { AlltouseService } from './../service/alltouse.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { ClipboardService } from 'ngx-clipboard';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  urlForm: FormGroup;

  isSpinning = false;
  isSpinning2 = false;
  returnedUrl = false;
  nodata1 = true;
  nodata2 = false;
  nodata3 = false;

  allUrlList = [];
  urlDetails: any;

  // Pagination
  pagination = 1;
  year: number;
  redirectUrl: any;

  constructor(
    private formBuilder: FormBuilder,
    private notification: NzNotificationService,
    private clipboardService: ClipboardService,
    private allToUseService: AlltouseService
  ) { }

  ngOnInit(): void {
    this.urlForm = this.formBuilder.group({
      typeUrl: ['', Validators.required],
      fullLink: ['', Validators.required],
      shortLink: ['', Validators.required],
    });

    this.getCurrentYear();
  }

  getUrl(): void{
    this.isSpinning = true;
    const str = this.urlForm.value.fullLink;
    if (str.startsWith('http://short.est')){
      this.allToUseService.decodeUrl(this.urlForm.value.fullLink).subscribe((result: any) => {
        this.urlDetails = result.fullLink;
        this.isSpinning = false;
        this.returnedUrl = true;
      }, error => {
        this.notification.error('Url', error.error.message || error.error.respMessage);
        this.isSpinning = false;
        this.returnedUrl = false;
      });
    } else {
      this.allToUseService.encodeUrl(this.urlForm.value.fullLink).subscribe((result: any) => {
        this.urlDetails = result.shortLink;
        this.isSpinning = false;
        this.returnedUrl = true;
      }, error => {
        this.notification.error('Url', error.error.message || error.error.respMessage);
        this.isSpinning = false;
        this.returnedUrl = false;
      });
    }
  }

  copyLinkUrl(): void{
    this.clipboardService.copyFromContent(this.urlDetails);
    this.notification.success('Url', 'Url Copied successfully!!!');
    this.urlForm.reset();
    this.returnedUrl = false;
  }

  getAllUrl(): void{
    this.nodata1 = false;
    this.nodata2 = true;
    this.nodata3 = false;
    this.allToUseService.getAllUrl().subscribe((result: any) => {
      this.allUrlList = result.links;
      this.notification.success('Url', 'Url fetch successfully !!');
      if (this.allUrlList.length === 0){
        this.nodata1 = false;
        this.nodata2 = false;
        this.nodata3 = true;
      }
    }, error => {
      this.nodata1 = true;
      this.nodata2 = false;
      this.nodata3 = false;
      this.notification.error('url', error.error.message || error.error.respMessage);
    });
  }

  getCurrentYear(): void{
    this.year = new Date().getFullYear();
  }

  openAliasLink(): void{
    this.isSpinning2 = true;
    this.allToUseService.redirectAlias(this.urlForm.value.typeUrl).subscribe((result: any) => {
      this.redirectUrl = result.fullLink;
      if (this.redirectUrl.includes('http')) {
        window.open(this.redirectUrl, '_blank');
        this.urlForm.reset();
        this.isSpinning2 = false;
      } else {
        window.open('http://' + this.redirectUrl, '_blank');
        this.urlForm.reset();
        this.isSpinning2 = false;
      }

    }, error => {
      this.isSpinning2 = false;
      this.notification.error('Url', error.error.message || error.error.respMessage);
    });
  }

}
