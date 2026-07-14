import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Recipe {
  id: number;
  name: string;
  description: string | null;
}

@Injectable({ providedIn: 'root' })
export class RecipeService {
  private readonly http = inject(HttpClient);

  getRecipes(): Observable<Recipe[]> {
    return this.http.get<Recipe[]>('/api/recipes');
  }
}
