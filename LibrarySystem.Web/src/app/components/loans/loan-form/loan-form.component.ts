import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LoanService } from '../../../services/loan.service';
import { Loan } from '../../../models/loan.model';
import { Book } from '../../../models/book.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-loan-form',
  templateUrl: './loan-form.component.html',
  //   styleUrls: ['./loan-form.component.css']
})
export class LoanFormComponent implements OnInit {
  loanForm: FormGroup;
  availableBooks: Book[] = [];
  isEditMode: boolean = false; // ADD THIS PROPERTY

  constructor(
    private fb: FormBuilder,
    private loanService: LoanService,
    private dialogRef: MatDialogRef<LoanFormComponent>,
    private snackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data: { loan: Loan | null; books: Book[] }
  ) {
    this.availableBooks = data.books.filter((book) => book.isAvailable);
    this.isEditMode = !!data.loan; // SET THIS
    this.loanForm = this.createForm();
  }

  ngOnInit(): void {
    // Implementation
  }

  createForm(): FormGroup {
    return this.fb.group({
      bookId: ['', Validators.required],
      userId: ['', Validators.required],
      userName: ['', [Validators.required, Validators.maxLength(100)]],
      loanDate: [new Date(), Validators.required],
    });
  }

  onSubmit(): void {
    if (this.loanForm.valid) {
      const loanData = {
        bookId: this.loanForm.value.bookId,
        userId: this.loanForm.value.userId,
        userName: this.loanForm.value.userName,
        loanDate: this.loanForm.value.loanDate,
      };

      this.loanService.createLoan(loanData).subscribe({
        next: () => {
          this.snackBar.open('Loan created successfully', 'Close', {
            duration: 3000,
          });
          this.dialogRef.close(true);
        },
        error: (error) => {
          this.snackBar.open('Error creating loan', 'Close', {
            duration: 3000,
          });
        },
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
