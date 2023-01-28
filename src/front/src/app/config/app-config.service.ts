import {Injectable} from "@angular/core";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {

  constructor() {
  }

  static getApiUrl(): string {
    return environment.apiUrl;
  }

  static getBaseHref(): string {
    return environment.baseHref;
  }
}
