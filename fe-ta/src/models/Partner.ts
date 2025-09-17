export interface PartnerRequest {
  name: string;
  logoUrl?: string;
  link?: string;
  orderIndex?: number;
  isActive?: boolean;
  createUid?: number;
  writeIUid?: number;
  updateTime?: string; // ISO 8601 format (e.g., "2025-09-16T12:00:00Z")
  note?: string;
  option1?: string;
  option2?: string;
  option3?: string;
  option4?: string;
  option5?: string;
}

export interface PartnerResponse {
  id: number;
  name: string;
  logoUrl?: string;
  link?: string;
  orderIndex: number;
  isActive: boolean;
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
