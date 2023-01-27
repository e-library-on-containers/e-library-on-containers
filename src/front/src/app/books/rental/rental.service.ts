import { Injectable } from '@angular/core';
import {AppConfigService} from "../../config/app-config.service";

@Injectable({
  providedIn: 'root'
})
export class RentalService {

  constructor(private apiUrl: String) {
    this.apiUrl = AppConfigService.getApiUrl()
  }
}
