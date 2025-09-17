import type { ProductMediaResponse } from "./ProductMedia";
import type { ProductSpecResponse } from "./ProductSpec";

export interface ProductRequest {
  categoryId: number;
  productName: string;
  slug: string;
  shortDescription?: string;
  longDescription?: string;
  price?: number;
  isFeatured?: boolean;
  isActive?: boolean;
  viewsCount?: number;
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

export interface ProductResponse {
  id: number;
  categoryId: number;
  productName: string;
  slug: string;
  shortDescription?: string;
  longDescription?: string;
  price?: number;
  isFeatured: boolean;
  isActive: boolean;
  viewsCount: number;
  createUid?: number;
  writeIUid?: number;
  updateTime?: string; 
  note?: string;
  option1?: string;
  option2?: string;
  option3?: string;
  option4?: string;
  option5?: string;

  mediaList?: ProductMediaResponse[];
  specs?: ProductSpecResponse[];
}