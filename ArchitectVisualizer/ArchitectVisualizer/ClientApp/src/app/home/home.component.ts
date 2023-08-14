import { Component,OnInit  } from '@angular/core';
import { graphviz }  from 'd3-graphviz';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  ngOnInit() {

      graphviz('#graph').renderDot('digraph {a -> b}');
  }
}
