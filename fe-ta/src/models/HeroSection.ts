import { de } from "element-plus/es/locales.mjs";
import type { HeroSectionProductResponse } from "./HeroSectionProduct";

export interface HeroSectionRequest {
  title?: string;
  description?: string;
  pageHero?: string;
  heroMediaFile?: File | null;
  isPublished?: boolean;
  publishFrom?: string;
  publishTo?: string;
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

export interface HeroSectionFilterRequest {
  title?: string;
  description?: string;
  pageHero?: string;
  isPublished?: boolean;
  publishFrom?: string;
  publishTo?: string;
}

export interface HeroSectionResponse {
  id: number;
  title?: string;
  description?: string;
  heroMediaUrl?: string;
  pageHero?: string;
  heroMediaType?: string;
  isPublished: boolean;
  publishFrom?: string; // ISO date string
  publishTo?: string; // ISO date string
  createUid?: number;
  writeIUid?: number;
  updateTime?: string; // ISO date string
  note?: string;
  option1?: string;
  option2?: string;
  option3?: string;
  option4?: string;
  option5?: string;

  heroProducts?: HeroSectionProductResponse[];
}
