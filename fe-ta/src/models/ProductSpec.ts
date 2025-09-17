export interface ProductSpecRequest {
  productId: number;
  specKey: string;
  specValue?: string;
  orderIndex?: number;
  createUid?: number;
  writeIUid?: number;
  updateTime?: string; // ISO string (e.g., "2025-09-16T10:00:00Z")
  note?: string;
  option1?: string;
  option2?: string;
  option3?: string;
  option4?: string;
  option5?: string;
}

export interface ProductSpecResponse {
  id: number;
  productId: number;
  specKey: string;
  specValue?: string;
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
