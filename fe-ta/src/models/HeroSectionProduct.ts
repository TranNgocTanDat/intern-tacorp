import type { HeroSectionResponse } from "./HeroSection";
import type { ProductResponse } from "./Product";

export interface HeroSectionProductRequest {
  heroSectionId: number;
  productId: number;
  orderIndex?: number;
}

export interface HeroSectionProductResponse {
  id: number;
  heroSectionId: number;
  productId: number;
  orderIndex: number;
  createUid?: number;
  createdName?: string;
  writeIUid?: number;
  updatedName?: string;
  updateTime?: string;
  note?: string;
  option1?: string;
  option2?: string;
  option3?: string;
  option4?: string;
  option5?: string;
  product?: ProductResponse;
  heroSection?: HeroSectionResponse;
}

export interface HeroSectionProductFilterRequest {
  heroSectionTitle?: string;
  productName?: string;

  createdName?: string;
  updatedName?: string;

  updateTimeFrom?: string;
  updateTimeTo?: string;

  note?: string;
}
