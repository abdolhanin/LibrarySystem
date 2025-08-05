import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Loan } from '../models/loan.model';

@Injectable({
  providedIn: 'root',
})
export class LoanService {
  private apiUrl = 'https://localhost:5168/api/loans';

  constructor(private http: HttpClient) {}

  getAllLoans(): Observable<Loan[]> {
    return this.http.get<Loan[]>(this.apiUrl);
  }

  getLoanById(id: string): Observable<Loan> {
    return this.http.get<Loan>(`${this.apiUrl}/${id}`);
  }

  createLoan(loan: Partial<Loan>): Observable<string> {
    return this.http.post<string>(this.apiUrl, loan);
  }

  returnLoan(id: string): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/return`, {});
  }

  deleteLoan(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
