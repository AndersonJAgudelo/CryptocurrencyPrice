import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { AppConfiguration } from "read-appsettings-json";
import { interval, Observable } from 'rxjs';
import { Cryptocurrency } from '../../models/Cryptocurrency';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})

export class FetchDataComponent {
  private readonly apiKey: string = AppConfiguration.Setting().ApiKey;
  private readonly source: Observable<number> = interval(5000);
  private readonly _http: HttpClient;
  private readonly _baseUrl: string;
  public cryptocurrency: Cryptocurrency[] = [];
  public selectedOption: string = "Bitcoin";
  public amount: number = 1.00;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this._http = http;
    this._baseUrl = baseUrl;
  }

  ngOnInit() {
    this.GetLastQuotes();
    this.source.subscribe(() => this.GetLastQuotes());
  }

  private GetLastQuotes = () => {
    const headers = new HttpHeaders({ 'X-CMC_PRO_API_KEY': this.apiKey });
    this._http.get<Cryptocurrency[]>(this._baseUrl + 'Cryptocurrency/GetLastQuotes', { headers }).subscribe(result => {
      this.cryptocurrency = result as Cryptocurrency[];
    }, error => console.error(error));
  }

  public PriceConversion = () => {
    var symbols = this.cryptocurrency.map(cryptocurrency => cryptocurrency.symbol);
    const headers = new HttpHeaders({ 'X-CMC_PRO_API_KEY': this.apiKey });
    this._http.get<Cryptocurrency[]>(this._baseUrl + 'Cryptocurrency/PriceConversion?id=' + this.selectedOption + '&symbols=' + symbols + '&amount=' + this.amount, { headers }).subscribe(result => {
        this.cryptocurrency = result as Cryptocurrency[];
      }, error => console.error(error));
  }
}
