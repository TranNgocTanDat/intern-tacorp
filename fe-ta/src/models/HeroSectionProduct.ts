export interface HeroSectionProductRequest {
  heroSectionId: number;
  productId: number;
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

export interface HeroSectionProductResponse {
  id: number;
  heroSectionId: number;
  productId: number;
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