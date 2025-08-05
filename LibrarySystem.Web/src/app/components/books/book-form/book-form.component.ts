import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BookService } from '../../../services/book.service';
import { Book } from '../../../models/book.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-book-form',
  template: `
    <h2 mat-dialog-title>{{ isEditMode ? 'Edit Book' : 'Add Book' }}</h2>

    <form [formGroup]="bookForm" (ngSubmit)="onSubmit()">
      <mat-dialog-content>
        <mat-form-field appearance="fill" class="full-width">
          <mat-label>Title</mat-label>
          <input
            matInput
            formControlName="title"
            placeholder="Enter book title"
          />
          <mat-error *ngIf="bookForm.get('title')?.hasError('required')">
            Title is required
          </mat-error>
        </mat-form-field>

        <mat-form-field appearance="fill" class="full-width">
          <mat-label>Author</mat-label>
          <input
            matInput
            formControlName="author"
            placeholder="Enter author name"
          />
          <mat-error *ngIf="bookForm.get('author')?.hasError('required')">
            Author is required
          </mat-error>
        </mat-form-field>

        <mat-form-field appearance="fill" class="full-width">
          <mat-label>ISBN</mat-label>
          <input matInput formControlName="isbn" placeholder="Enter ISBN" />
          <mat-error *ngIf="bookForm.get('isbn')?.hasError('required')">
            ISBN is required
          </mat-error>
        </mat-form-field>

        <mat-form-field appearance="fill" class="full-width">
          <mat-label>Published Date</mat-label>
          <input
            matInput
            [matDatepicker]="picker"
            formControlName="publishedDate"
          />
          <mat-datepicker-toggle
            matSuffix
            [for]="picker"
          ></mat-datepicker-toggle>
          <mat-datepicker #picker></mat-datepicker>
        </mat-form-field>

        <mat-checkbox formControlName="isAvailable">Available</mat-checkbox>
      </mat-dialog-content>

      <mat-dialog-actions>
        <button mat-button type="button" (click)="onCancel()">Cancel</button>
        <button
          mat-raised-button
          color="primary"
          type="submit"
          [disabled]="!bookForm.valid"
        >
          {{ isEditMode ? 'Update' : 'Create' }}
        </button>
      </mat-dialog-actions>
    </form>
  `,
  styles: [
    `
      .full-width {
        width: 100%;
        margin-bottom: 15px;
      }
      mat-dialog-content {
        min-width: 400px;
        padding: 20px 0;
      }
    `,
  ],
})
export class BookFormComponent implements OnInit {
  bookForm: FormGroup;
  isEditMode: boolean = false;

  constructor(
    private fb: FormBuilder,
    private bookService: BookService,
    private dialogRef: MatDialogRef<BookFormComponent>,
    private snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data: Book
  ) {
    this.isEditMode = !!data;
    this.bookForm = this.createForm();
  }

  ngOnInit(): void {
    if (this.isEditMode && this.data) {
      this.bookForm.patchValue({
        ...this.data,
        publishedDate: new Date(this.data.publishedDate),
      });
    }
  }

  createForm(): FormGroup {
    return this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      author: ['', [Validators.required, Validators.maxLength(100)]],
      isbn: ['', [Validators.required]],
      publishedDate: ['', Validators.required],
      isAvailable: [true],
    });
  }

  onSubmit(): void {
    if (this.bookForm.valid) {
      const book: Book = this.bookForm.value;

      if (this.isEditMode) {
        this.bookService.updateBook(this.data.id!, book).subscribe({
          next: () => {
            this.snackBar.open('Book updated successfully', 'Close', {
              duration: 3000,
            });
            this.dialogRef.close(true);
          },
          error: (error) => {
            this.snackBar.open('Error updating book', 'Close', {
              duration: 3000,
            });
          },
        });
      } else {
        this.bookService.createBook(book).subscribe({
          next: () => {
            this.snackBar.open('Book created successfully', 'Close', {
              duration: 3000,
            });
            this.dialogRef.close(true);
          },
          error: (error) => {
            this.snackBar.open('Error creating book', 'Close', {
              duration: 3000,
            });
          },
        });
      }
    }
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
