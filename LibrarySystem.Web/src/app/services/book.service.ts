import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Book } from '../models/book.model';

@Injectable({
  providedIn: 'root',
})
export class BookService {
  private apiUrl = `${environment.apiUrl}/books`;

  constructor(private http: HttpClient) {}

  // Get available books
  getAllBooks(): Observable<Book[]> {
    return this.http
      .get<Book[]>(`${this.apiUrl}/available`)
      .pipe(catchError(this.handleError));
  }

  getBookById(id: string): Observable<Book> {
    return this.http
      .get<Book>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.handleError));
  }

  createBook(book: any): Observable<any> {
    const command = {
      title: book.title,
      author: book.author,
      isbn: book.isbn,
      publishedDate: book.publishedDate,
    };

    return this.http
      .post<any>(this.apiUrl, command)
      .pipe(catchError(this.handleError));
  }

  updateBook(id: string, book: any): Observable<any> {
    const command = {
      id: id,
      title: book.title,
      author: book.author,
      isbn: book.isbn,
      publishedDate: book.publishedDate,
    };

    return this.http
      .put<any>(`${this.apiUrl}/${id}`, command)
      .pipe(catchError(this.handleError));
  }

  archiveBook(id: string): Observable<any> {
    return this.http
      .patch<any>(`${this.apiUrl}/${id}/archive`, {})
      .pipe(catchError(this.handleError));
  }

  lendBook(id: string): Observable<any> {
    return this.http
      .post<any>(`${this.apiUrl}/${id}/lend`, {})
      .pipe(catchError(this.handleError));
  }

  returnBook(id: string): Observable<any> {
    return this.http
      .post<any>(`${this.apiUrl}/${id}/return`, {})
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    console.error('API Error:', error);
    let errorMessage = 'An unknown error occurred';

    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    return throwError(() => new Error(errorMessage));
  }
}
