export interface ProductMediaRequest {
  productId: number;
  mediaFileUrl?: string;
  mediaType?: string; // "image" or "video"
  isPrimary?: boolean;
  orderIndex?: number;
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

export interface ProductMediaResponse {
  id: number;
  productId: number;
  mediaFileUrl?: string;
  mediaType?: string;
  isPrimary: boolean;
  orderIndex: number;
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
