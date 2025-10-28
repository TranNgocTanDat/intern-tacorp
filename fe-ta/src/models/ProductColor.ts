import type { ProductMediaResponse } from "./ProductMedia";

/** Dùng khi thêm/sửa màu sản phẩm */
export interface ProductColorRequest {
  productId: number;
  colorName: string;       
  colorCode?: string;      
  isAvailable?: boolean;   
  note?: string;
}

/** Dùng khi lọc màu sản phẩm trong admin */
export interface ProductColorFilterRequest {
  productId: number;
  productName?: string;
  colorName?: string;
  colorCode?: string;
  isAvailable?: boolean;
  createdName?: string;
  updatedName?: string;
  fromUpdateTime?: string;
  toUpdateTime?: string;
  note?: string;
}

/** Dữ liệu trả về từ API — hiển thị ở UI */
export interface ProductColorResponse {
  id: number;
  productId: number;
  productName?: string;
  colorName: string;
  colorCode?: string;
  isAvailable: boolean;
  createUid?: number;
  writeIUid?: number;
  updateTime?: string;
  createdName?: string;
  updatedName?: string;
  note?: string;
  mediaList?: ProductMediaResponse[];
}