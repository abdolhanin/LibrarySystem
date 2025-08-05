import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
    <div class="app-container">
      <app-book-list></app-book-list>
    </div>
  `,
  styles: [
    `
      .app-container {
        min-height: 100vh;
        background-color: #fafafa;
      }
    `,
  ],
})
export class AppComponent {
  title = 'Library System';
}
