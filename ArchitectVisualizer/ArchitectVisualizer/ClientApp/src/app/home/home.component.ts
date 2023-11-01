import { Component,OnInit,Inject  } from '@angular/core';
import { graphviz }  from 'd3-graphviz';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  public graphText: string = "";
  private _baseUrl: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._baseUrl = baseUrl;
  }

  ngOnInit() {
    let result = this.http.get<GraphResult>(this._baseUrl + 'graph');
    result.subscribe((data: GraphResult) => graphviz('#graph').renderDot(data.text));
  }
}
interface GraphResult {
  text: string;
}
