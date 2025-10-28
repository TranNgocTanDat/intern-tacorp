import type { PartnerResponse } from "./Partner";
import type { ProductResponse } from "./Product";

export interface CategoryRequest {
  name: string;
  slug: string;
  parentId?: number;
  partnerId?: number;
  description?: string;
  orderIndex?: number;
  isActive?: boolean;
  note?: string;
}

export interface CategoryResponse {
  id: number;
  name: string;
  slug: string;
  parentId?: number;
  partnerId?: number;
  description?: string;
  orderIndex: number;
  isActive: boolean;
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
  children?: CategoryResponse[];
  products?: ProductResponse[];
  partner?: PartnerResponse;
}

export interface CategoryFilterRequest {
  name?: string;
  description?: string;
  isActive?: boolean;
  parentId?: number;
  createdName?: string;
  updatedName?: string;
  updateTimeFrom?: string;
  updateTimeTo?: string;
  note?: string;
}
