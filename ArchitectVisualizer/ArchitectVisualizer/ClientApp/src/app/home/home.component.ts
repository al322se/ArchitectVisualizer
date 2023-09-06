import { Component,OnInit,Inject  } from '@angular/core';
import { graphviz }  from 'd3-graphviz';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  public graphText: string="";

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<GraphResult>(baseUrl + 'graph').subscribe(result => {
      this.graphText = result.text;
    }, error => console.error(error));
  }

  ngOnInit() {

      graphviz('#graph').renderDot(this.graphText);
  }
}
interface GraphResult {
  text: string;
}
