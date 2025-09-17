import type { ProductResponse } from "./Product";

export interface CategoryRequest {
  name: string;
  slug: string;
  parentId?: number;
  description?: string;
  orderIndex?: number;
  isActive?: boolean;
  createUid?: number;
  writeIUid?: number;
  updateTime?: string; 
  note?: string;
  option1?: string;
  option2?: string;
  option3?: string;
  option4?: string;
  option5?: string;
}

export interface CategoryResponse {
  id: number;
  name: string;
  slug: string;
  parentId?: number;
  description?: string;
  orderIndex: number;
  isActive: boolean;
  createUid?: number;
  writeIUid?: number;
  updateTime?: string;
  note?: string;
  option1?: string;
  option2?: string;
  option3?: string;
  option4?: string;
  option5?: string;
  children?: CategoryResponse[];
  products?: ProductResponse[];
}