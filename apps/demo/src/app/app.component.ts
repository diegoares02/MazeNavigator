import { Component, OnInit } from '@angular/core';
import { LoggingService } from './logging/logging.service';
import { StuffService } from './stuff/stuff.service';
import { ControlServiceService } from './service/control-service.service';

@Component({
  selector: 'valant-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less'],
})
export class AppComponent implements OnInit {
  public title = 'Valant demo';
  public data: string[];
  public fileName: string;
  public template: string[][] = [];
  public directions: string[];
  public positionTemplate: any;
  public templateString: string;
  public showSuccessAlert : boolean;
  constructor(
    private logger: LoggingService,
    private stuffService: StuffService,
    private controlService: ControlServiceService
  ) {}

  ngOnInit() {
    this.logger.log('Welcome to the AppComponent');
    this.getStuff();
    this.getDirections();
  }

  private getStuff(): void {
    this.stuffService.getStuff().subscribe({
      next: (response: string[]) => {
        this.data = response;
      },
      error: (error) => {
        this.logger.error('Error getting stuff: ', error);
      },
    });
  }

  public Move(e: any): void {
    const data = {
      direction: e,
      maze: this.templateString,
    };
    this.controlService.move(JSON.stringify(data)).subscribe((t) => {   
      const temp = [];   
      const test = t.split('||');
      test.forEach((el) => {
        temp.push([...el]);
      });
      temp.pop(); //Remove last value
      this.template = temp;
      this.templateString = temp.map((row) => row.join('')).join('||');
      this.showSuccessAlert = !t.includes('E');
    });
  }
  public Test(e:any):void{
    console.log(e)
  }

  private getDirections(): void {
    this.controlService.getControls().subscribe((temp) => (this.directions = temp));
  }

  public onFileSelected(event) {
    const file: File = event.target.files[0];
    if (file) {
      this.fileName = file.name;
      const formData = new FormData();
      formData.append('file', file);
      this.controlService.uploadMaze(formData).subscribe((t) => {
        const test = t.split('||');
        test.forEach((el) => {
          this.template.push([...el]);
        });
        this.template.pop(); //Remove last value
        this.templateString = this.template.map((row) => row.join('')).join('||');
      });
    }
  }
}
