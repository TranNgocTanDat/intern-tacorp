export interface ProductMediaRequest {
  mediaFileUrl?: File | null;
  colorId?: number ;
  mediaType?: string; // "image" or "video"
  descriptionMedia?: string;
  isPrimary?: boolean;
  orderIndex?: number;
  note?: string;
}

export interface ProductMediaFilterRequest {
  productName?: string;
  colorName?: string;
  mediaFileUrl?: string;
  mediaType?: string; // "image" | "video"
  descriptionMedia?: string;
  isPrimary?: boolean;
  createdName?: string;
  updatedName?: string;
  fromUpdateTime?: string;
  toUpdateTime?: string;
  note?: string;
}

export interface ProductMediaResponse {
  id: number;
  productId: number;
  mediaFileUrl?: string;
  colorId: number ;
  colorName?: string;
  mediaType?: string;
  descriptionMedia?: string;
  isPrimary: boolean;
  orderIndex: number;
  createUid?: number;
  writeIUid?: number;
  createdName?: string;
  updatedName?: string;
  updateTime?: string;
  note?: string;
  option1?: string;
  option2?: string;
  option3?: string;
  option4?: string;
  option5?: string;
}
