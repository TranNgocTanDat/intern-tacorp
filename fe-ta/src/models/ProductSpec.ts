export interface ProductSpecRequest {
  specKey: string;
  specValue?: string;
  orderIndex?: number;
  note?: string;
}

export interface ProductSpecFilterRequest {
  productId?: number;
  productName?: string;

  specKey?: string;
  specValue?: string;
  orderIndex?: number;

  createdName?: string;
  updatedName?: string;

  fromUpdateTime?: string; // ISO string
  toUpdateTime?: string; // ISO string
  note?: string;
}

export interface ProductSpecResponse {
  id: number;
  productId: number;
  productName?: string;
  specKey: string;
  specValue?: string;
  orderIndex: number;
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
}
