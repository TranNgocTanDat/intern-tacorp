import type { ProductColorResponse } from "./ProductColor";
import type { ProductMediaResponse } from "./ProductMedia";
import type { ProductSpecResponse } from "./ProductSpec";
import type { ProductStorageResponse } from "./ProductStorage";

export interface ProductRequest {
  categoryId?: number;
  productName: string;
  slug: string;
  shortDescription?: string;
  longDescription?: string;
  originalPrice?: number;
  discountPrice?: number;
  isFeatured?: boolean;
  isActive?: boolean;
  viewsCount?: number;
  note?: string;

}

export interface ProductResponse {
  id: number;
  categoryId: number;
  categoryName?: string;
  productName: string;
  slug: string;
  shortDescription?: string;
  longDescription?: string;
  originalPrice?: number;
  discountPrice?: number;
  discount?: number;
  isFeatured: boolean;
  isActive: boolean;
  viewsCount: number;
  createUid?: number;
  writeIUid?: number;
  updateTime?: string; 
  createdName?: string;
  updatedName?: string;
  note?: string;
  option1?: string;
  option2?: string;
  option3?: string;
  option4?: string;
  option5?: string;

  mediaList?: ProductMediaResponse[];
  specs?: ProductSpecResponse[];
  colors?: ProductColorResponse[];
  storages?: ProductStorageResponse[];
}

export interface ProductFilterRequest {
  productName?: string;
  categoryName?: string;
  slug?: string;
  shortDescription?: string;
  longDescription?: string;

  // Lọc theo khoảng giá
  minPrice?: number;
  maxPrice?: number;

  isFeatured?: boolean;
  isActive?: boolean;

  // Lọc theo views
  minViewsCount?: number;
  maxViewsCount?: number;

  // User tạo/sửa
  createdName?: string;
  updatedName?: string;

  // Lọc theo thời gian
  fromUpdateTime?: string; // ISO string (yyyy-MM-dd)
  toUpdateTime?: string;

  note?: string;
}