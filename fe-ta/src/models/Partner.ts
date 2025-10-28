export interface PartnerRequest {
  name: string;
  logoFile?: File | null;
  imgDefaultFile?: File | null;
  imgHoverFile?: File | null;
  slug?: string;
  link?: string;
  orderIndex?: number;
  isActive?: boolean;
  note?: string;

}

export interface PartnerFilterRequest {
  name?: string;
  isActive?: boolean;
  createdName?: string;
  updatedName?: string;
  updateTimeFrom?: string;
  updateTimeTo?: string;
  note?: string;
}

export interface PartnerResponse {
  id: number;
  name: string;
  logoUrl?: string;
  imgDefaultUrl?: string;
  imgHoverUrl?: string;
  slug?: string;
  link?: string;
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
}
